using System.IO;
using ManiaPlanetSharp.TMUnlimiter;
using VersionBackendUnlimiter13 = ManiaPlanetSharp.TMUnlimiter.Version13.VersionBackend;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.TMUnlimiter
{
    [Chunk(0x3f_004_00fu, skippable: true)]
    public class LegacyScriptChunk : Chunk
    {
        [Property, CustomParserMethod(nameof(Archive))]
        public LegacyScript LegacyScript { get; set; }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        public LegacyScript Archive( GameBoxReader reader )
        {
            reader.Stream.Seek( -4, System.IO.SeekOrigin.Current );
            // Chunk size is an unsigned integer, but MemoryStream works on integers instead...
            int chunkSize = reader.ReadInt32();

            // Decrypt weakly encrypted TMUnlimiter 1.3 chunk
            byte[] cryptedChunkData = VersionBackendUnlimiter13.DecryptChunkData( reader.ReadRaw( chunkSize ) );

            using ( GameBoxReader innerReader = new GameBoxReader( new MemoryStream( cryptedChunkData ) ) )
            {
                LegacyScriptExecutionType executionType;

                // Match legacy script execution type
                switch ( innerReader.ReadByte() )
                {
                    case 1:
                    {
                        executionType = LegacyScriptExecutionType.TriggerAlways;
                        break;
                    }
                    case 2:
                    {
                        executionType = LegacyScriptExecutionType.TriggerInside;
                        break;
                    }
                    default:
                    {
                        executionType = LegacyScriptExecutionType.TriggerOnce;
                        break;
                    }
                }

                // Chunk size is an unsigned integer, but MemoryStream works on integers instead...
                return new LegacyScript( innerReader.ReadRaw( innerReader.ReadInt32() ), executionType );
            }
        }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
    }
}