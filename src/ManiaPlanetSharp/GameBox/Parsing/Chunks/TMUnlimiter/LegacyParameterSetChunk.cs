using System.IO;
using ManiaPlanetSharp.TMUnlimiter;
using VersionBackendUnlimiter13 = ManiaPlanetSharp.TMUnlimiter.Version13.VersionBackend;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.TMUnlimiter
{
    [Chunk(0x3f_003_00fu, skippable: true)]
    public class LegacyParameterSetChunk : Chunk
    {
        [Property, CustomParserMethod(nameof(Archive))]
        public ParameterSet ParameterSet { get; set; }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        public ParameterSet Archive( GameBoxReader reader )
        {
            reader.Stream.Seek( -4, System.IO.SeekOrigin.Current );
            // Chunk size is an unsigned integer, but MemoryStream works on integers instead...
            int chunkSize = reader.ReadInt32();

            // Decrypt weakly encrypted TMUnlimiter 1.3 chunk
            byte[] cryptedChunkData = VersionBackendUnlimiter13.DecryptChunkData( reader.ReadRaw( chunkSize ) );

            using ( GameBoxReader innerReader = new GameBoxReader( new MemoryStream( cryptedChunkData ) ) )
            {
                ParameterSet parameterSet = new ParameterSet();
                uint parameterCount = innerReader.ReadUInt32();

                for ( uint parameterIndex = 0; parameterIndex < parameterCount; parameterIndex++ )
                {
                    ParameterFunction parameterFunction = VersionBackendUnlimiter13.GetParameterFunction( innerReader.ReadByte(), innerReader.ReadByte() );

                    if ( parameterFunction == null )
                    {
                        continue;
                    }

                    switch ( parameterFunction.ValueType )
                    {
                        case ParameterValueType.Number:
                        {
                            parameterSet.Parameters.Add( new ParameterNumber( parameterFunction, innerReader.ReadFloat() ) );
                            break;
                        }
                        case ParameterValueType.String:
                        {
                            parameterSet.Parameters.Add( new ParameterString( parameterFunction, innerReader.ReadString( innerReader.ReadByte() ) ) );
                            break;
                        }
                        default:
                        {
                            parameterSet.Parameters.Add( new Parameter( parameterFunction ) );
                            break;
                        }
                    }
                }

                return parameterSet.Parameters.Count > 0 ? parameterSet : null;
            }
        }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
    }
}