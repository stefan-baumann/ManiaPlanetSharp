using System;
using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;

namespace ManiaPlanetSharp.TMUnlimiter
{
    public abstract class VersionBackend
    {
        protected abstract class Chunk03043055
        {
            protected abstract void ArchiveBlock( GameBoxReader reader, uint blockIndex );
            protected abstract void SetDecorationOffset( int offsetX, int offsetY, int offsetZ );
            protected abstract void SetSkyOnlyDecorationVisibility( bool skyOnlyDecorationVisibility );
            protected abstract ParameterFunction GetParameterFunction( uint catalogIndex, uint functionIndex );
            protected abstract void AddLegacyMediaClipMapping( uint mediaClipIndex, object resource );

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
            public void Archive( GameBoxReader reader )
            {
                this.SetDecorationOffset( reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32() );
                this.SetSkyOnlyDecorationVisibility( reader.ReadByte() != 0 );

                uint blockDataCount = reader.ReadUInt32();

                while ( blockDataCount-- != 0 )
                {
                    this.ArchiveBlock( reader, reader.ReadUInt32() );
                }

                uint mediaClipMappingCount = reader.ReadUInt32();

                while ( mediaClipMappingCount-- != 0 )
                {
                    uint mediaClipIndex = reader.ReadUInt32();
                    byte resourceType = reader.ReadByte();

                    switch ( resourceType )
                    {
                        case 0: // Parameter Set
                        {
                            ParameterSet parameterSet = new ParameterSet();

                            for ( uint parameterIndex = 0; parameterIndex < 4; parameterIndex++ )
                            {
                                ParameterFunction parameterFunction = this.GetParameterFunction( reader.ReadByte(), reader.ReadByte() );
                                float value = reader.ReadFloat();

                                if ( parameterFunction == null )
                                {
                                    continue;
                                }

                                if ( parameterFunction.ValueType == ParameterValueType.Number )
                                {
                                    parameterSet.Parameters.Add( new ParameterNumber( parameterFunction, value ) );
                                }
                                else
                                {
                                    parameterSet.Parameters.Add( new Parameter( parameterFunction ) );
                                }
                            }

                            if ( parameterSet.Parameters.Count > 0 )
                            {
                                this.AddLegacyMediaClipMapping( mediaClipIndex, parameterSet );
                            }

                            break;
                        }
                        case 1: // Legacy Script
                        {
                            // Byte code size is an unsigned integer, but MemoryStream works on integers instead...
                            int byteCodeSize = reader.ReadInt32();

                            if ( byteCodeSize != 0 )
                            {
                                this.AddLegacyMediaClipMapping( mediaClipIndex, new LegacyScript( reader.ReadRaw( byteCodeSize ), LegacyScriptExecutionType.TriggerOnce ) );
                            }

                            break;
                        }
                        default: // Unknown
                        {
                            throw new NotSupportedException( $"Unknown resource type (resourceType = {resourceType}). Only parameter sets and legacy scripts are supported." );
                        }
                    }
                }

                // Skip fake 0xfacade01
                reader.Skip( 4 );
            }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        }

        public abstract TrackVersion GetTrackVersion();

        // Read data saved from legacy version of TMUnlimiter (1.3, 1.2 and 1.1)
        public virtual void ArchiveOld( GameBoxReader reader )
        {
            // By default function is empty and ready for override
        }

        // Read data saved from modern version of TMUnlimiter (^2.0.0)
        public virtual void ArchiveNew( GameBoxReader reader, uint archiveVersion )
        {
            // No need to implement this method (at the moment)
        }

        // Get additional data associated with each block
        public virtual BlockData[] GetBlocksData()
        {
            return null;
        }

        public virtual bool IsTrackBaseEmpty()
        {
            return false;
        }

        public virtual bool IsPylonsDisabled()
        {
            return false;
        }

        public virtual bool IsDecorationOffsetApplied()
        {
            return false;
        }

        public virtual bool IsDecorationScaleApplied()
        {
            return false;
        }

        public virtual Vector3D GetDecorationOffset()
        {
            return new Vector3D();
        }

        public virtual Vector3D GetDecorationScale()
        {
            return new Vector3D( 1.0f, 1.0f, 1.0f );
        }

        public virtual DecorationVisibility GetDecorationVisibility()
        {
            return DecorationVisibility.Default;
        }

        public virtual LegacyMediaClipResource[] GetLegacyMediaClipResources()
        {
            return null;
        }
    }
}