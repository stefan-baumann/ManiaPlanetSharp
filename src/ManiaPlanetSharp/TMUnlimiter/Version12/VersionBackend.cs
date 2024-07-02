using System.Collections.Generic;
using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;

namespace ManiaPlanetSharp.TMUnlimiter.Version12
{
    public class VersionBackend : TMUnlimiter.VersionBackend
    {
        public static readonly List<ParameterFunction> VehicleParameterFunctions = new List<ParameterFunction>();
        public static readonly List<ParameterFunction> ResetParameterFunctions = new List<ParameterFunction>();
        public static readonly List<ParameterFunction> ClipCommunicationParameterFunctions = new List<ParameterFunction>();

        static VersionBackend()
        {
            VehicleParameterFunctions.Add( null ); // "None" function
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle gravity (new method)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle gravity (old method)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle gravity (simple invert)", ParameterValueType.None ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle max speed (forward)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle max speed (backward)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change yellow booster multipiler", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change red booster multipiler", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change acceleration multipiler", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change acceleration multipiler (new)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change yellow booster effect time", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change red booster effect time", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( null ); // "Change vehicle property (Locked)" function
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change drift deceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change air deceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change steering strength", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change steepness acceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change roof deceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle grip", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change free wheeling brake", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle brake", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change water bounce", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change vehicle air stability", ParameterValueType.Number ) );

            ResetParameterFunctions.Add( null ); // "None" function
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset all vehicle values to default", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle gravity", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle max speed", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle max speed (forward)", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle max speed (backward)", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset booster multipiler value", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset yellow booster multipiler value", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset red booster multipiler value", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset acceleration multipiler", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset booster effect time", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset yellow booster effect time", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset red booster effect time", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset drift deceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset air deceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset steering strength", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset steepness acceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset roof deceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle grip", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset free wheeling brake", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle brake", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset water bounce", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset vehicle air stability", ParameterValueType.None ) );

            ClipCommunicationParameterFunctions.Add( null ); // "None" function
            ClipCommunicationParameterFunctions.Add( new ParameterFunction( "Clip Communication/Communicate with another clip", ParameterValueType.Number ) );
            ClipCommunicationParameterFunctions.Add( new ParameterFunction( "Clip Communication/Communicate with another clip and display", ParameterValueType.Number ) );
            ClipCommunicationParameterFunctions.Add( new ParameterFunction( "Clip Communication/Add script to update list", ParameterValueType.Number ) );
            ClipCommunicationParameterFunctions.Add( new ParameterFunction( "Clip Communication/Remove script from update list", ParameterValueType.Number ) );
        }

        public static ParameterFunction GetParameterFunction( uint catalogIndex, uint functionIndex )
        {
            switch ( catalogIndex )
            {
                case 1: // "Vehicle"
                {
                    return
                    (
                        functionIndex >= VehicleParameterFunctions.Count
                        ?
                        null
                        :
                        VehicleParameterFunctions[ ( int )functionIndex ]
                    );
                }
                case 2: // "Reset" catalog
                {
                    return
                    (
                        functionIndex >= ResetParameterFunctions.Count
                        ?
                        null
                        :
                        ResetParameterFunctions[ ( int )functionIndex ]
                    );
                }
                case 3: // "Clip Communication" catalog
                {
                    return
                    (
                        functionIndex >= ClipCommunicationParameterFunctions.Count
                        ?
                        null
                        :
                        ClipCommunicationParameterFunctions[ ( int )functionIndex ]
                    );
                }
                default: // Unknown catalog
                {
                    return null;
                }
            }
        }

        protected new class Chunk03043055 : TMUnlimiter.VersionBackend.Chunk03043055
        {
            VersionBackend VersionBackend { get; set; }

            public Chunk03043055( VersionBackend versionBackend )
            {
                this.VersionBackend = versionBackend;
            }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
            protected override void ArchiveBlock( GameBoxReader reader, uint blockIndex )
            {
                BlockData blockData = new BlockData( blockIndex );

                blockData.InternalOverOverSizeChunkX = reader.ReadByte();
                blockData.InternalOverOverSizeChunkY = reader.ReadByte();
                blockData.InternalOverOverSizeChunkZ = reader.ReadByte();
                blockData.InternalIsInverted = reader.ReadByte() != 0;
                blockData.InternalBlockOffsetX = reader.ReadInt32();
                blockData.InternalBlockOffsetY = reader.ReadInt32();
                blockData.InternalBlockOffsetZ = reader.ReadInt32();
                blockData.InternalBlockRotationX = reader.ReadUInt32();
                blockData.InternalBlockRotationY = reader.ReadUInt32();
                blockData.InternalBlockRotationZ = reader.ReadUInt32();

                this.VersionBackend.BlocksData.Add( blockData );
            }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.

            protected override void SetDecorationOffset( int offsetX, int offsetY, int offsetZ )
            {
                this.VersionBackend.DecorationOffsetX = offsetX;
                this.VersionBackend.DecorationOffsetY = offsetY;
                this.VersionBackend.DecorationOffsetZ = offsetZ;
            }

            protected override void SetSkyOnlyDecorationVisibility( bool skyOnlyDecorationVisibility )
            {
                this.VersionBackend.DecorationVisibility_SkyOnly = skyOnlyDecorationVisibility;
            }

            protected override ParameterFunction GetParameterFunction( uint catalogIndex, uint functionIndex )
            {
                return VersionBackend.GetParameterFunction( catalogIndex, functionIndex );
            }

            protected override void AddLegacyMediaClipMapping( uint mediaClipIndex, object resource )
            {
                this.VersionBackend.LegacyMediaClipResources.Add( new LegacyMediaClipResource( mediaClipIndex, resource ) );
            }
        }

        bool DecorationVisibility_SkyOnly;
        int DecorationOffsetX;
        int DecorationOffsetY;
        int DecorationOffsetZ;
        readonly List<BlockData> BlocksData = new List<BlockData>();
        readonly List<LegacyMediaClipResource> LegacyMediaClipResources = new List<LegacyMediaClipResource>();

        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter12;
        }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        public override void ArchiveOld( GameBoxReader reader )
        {
            new Chunk03043055( this ).Archive( reader );
        }

        public override void ArchiveNew( GameBoxReader reader, uint archiveVersion )
        {
            // No need to implement this chunk (at the moment)
        }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.

        public override TMUnlimiter.BlockData[] GetBlocksData()
        {
            return this.BlocksData.ToArray();
        }

        public override bool IsDecorationOffsetApplied()
        {
            return this.DecorationOffsetX != 0 || this.DecorationOffsetY != 0 || this.DecorationOffsetZ != 0;
        }

        public override Vector3D GetDecorationOffset()
        {
            return new Vector3D( this.DecorationOffsetX, this.DecorationOffsetY, this.DecorationOffsetZ );
        }

        public override DecorationVisibility GetDecorationVisibility()
        {
            if ( this.DecorationVisibility_SkyOnly )
            {
                return DecorationVisibility.SkyOnly;
            }
            else
            {
                return DecorationVisibility.Default;
            }
        }

        public override LegacyMediaClipResource[] GetLegacyMediaClipResources()
        {
            return this.LegacyMediaClipResources.ToArray();
        }
    }
}