using System;
using System.IO;
using ManiaPlanetSharp.TMUnlimiter;
using VersionBackendUnlimiter13 = ManiaPlanetSharp.TMUnlimiter.Version13.VersionBackend;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.TMUnlimiter
{
    public abstract class ChallengeBackendChunk : Chunk
    {
        [Property, CustomParserMethod(nameof(Archive))]
        public ChallengeBackend ChallengeBackend { get; set; }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        public ChallengeBackend Archive(GameBoxReader reader)
        {
            uint archivingChunkId = this.GetArchivingChunkId();

            switch ( archivingChunkId )
            {
                // TMUnlimiter 1.1 and 1.2 chunk
                case 0x03_043_055u:
                {
                    // Check that the skip marker has been written (since ManiaPlanet maps) to skip unnecessary exception.
                    if ( reader.ReadUInt32() == GameBoxReader.SkipMarker )
                    {
                        reader.Skip( reader.ReadInt32() );
                        return null;
                    }
                    else
                    {
                        reader.Stream.Seek( -4, SeekOrigin.Current );
                    }

                    byte chunkVersion = reader.ReadByte();

                    switch ( chunkVersion )
                    {
                        // Version 0 from TMUnlimiter 1.1
                        case 1:
                        {
                            ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter11 );
                            break;
                        }
                        // Version 1 from TMUnlimiter 1.2
                        case 2:
                        {
                            ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter12 );
                            break;
                        }
                        default:
                        {
                            throw new NotSupportedException( $"Unknown version of chunk 0x03043055 (version = {chunkVersion}). This chunk only supports versions 1 and 2." );
                        }
                    }

                    ChallengeBackend.ArchiveOld( reader );
                    break;
                }
                // TMUnlimiter 1.3 chunk (skippable)
                case 0x3f_001_000u:
                {
                    ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter13 );

                    reader.Stream.Seek( -4, SeekOrigin.Current );
                    // Chunk size is an unsigned integer, but MemoryStream works on integers instead...
                    int chunkSize = reader.ReadInt32();

                    // Decrypt weakly encrypted TMUnlimiter 1.3 chunk
                    byte[] cryptedChunkData = VersionBackendUnlimiter13.DecryptChunkData( reader.ReadRaw( chunkSize ) );

                    // Parse TMUnlimiter 1.3 chunk properly. A separate GameBoxReader instance is used because
                    // there is no method to initialize a nested GameBoxReader instance from a byte array.
                    // Anyway, this does not affect parsing in any way, as legacy TMUnlimiter chunks did
                    // not rely on any MwId strings, internal or external references.
                    using ( GameBoxReader innerReader = new GameBoxReader( new MemoryStream( cryptedChunkData ) ) )
                    {
                        ChallengeBackend.ArchiveOld( innerReader );
                    }

                    break;
                }
                // TMUnlimiter 2.0 chunks (skippable)
                case 0x3f_001_001u:
                case 0x3f_001_002u:
                case 0x3f_001_003u:
                {
                    reader.Stream.Seek( -4, SeekOrigin.Current );
                    // Chunk size is an unsigned integer, but MemoryStream works on integers instead...
                    int chunkSize = reader.ReadInt32();

                    using ( GameBoxReader innerReader = reader.GetNestedLengthLimitedReader( chunkSize ) )
                    {
                        uint archiveVersion = archivingChunkId - 0x3f_001_001;
                        byte primaryTrackVersion = innerReader.ReadByte();

                        switch ( primaryTrackVersion )
                        {
                            // Vanilla track version
                            case 0:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Vanilla );
                                break;
                            }
                            // TMUnlimiter 0.4 track version
                            case 1:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter04 );
                                break;
                            }
                            // TMUnlimiter 0.6 track version
                            case 2:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter06 );
                                break;
                            }
                            // TMUnlimiter 0.7 track version
                            case 3:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter07 );
                                break;
                            }
                            // TMUnlimiter 1.1 track version
                            case 4:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter11 );
                                break;
                            }
                            // TMUnlimiter 1.2 track version
                            case 5:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter12 );
                                break;
                            }
                            // TMUnlimiter 1.3 track version
                            case 6:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter13 );
                                break;
                            }
                            // TMUnlimiter 2.0 track version
                            case 7:
                            {
                                ChallengeBackend = new ChallengeBackend( TrackVersion.Unlimiter20 );
                                break;
                            }
                            default:
                            {
                                throw new NotSupportedException( $"Unsupported track version = { primaryTrackVersion }" );
                            }
                        }

                        ChallengeBackend.ArchiveNew( innerReader, archiveVersion );
                    }

                    break;
                }
                // Unknown chunk
                default:
                {
                    throw new NotSupportedException( $"Unknown chunk \"0x{archivingChunkId:X8}\"" );
                }
            }

            return ChallengeBackend;
        }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.

        protected abstract uint GetArchivingChunkId();
    }

    [Chunk(0x03_043_055u, skippable: false)]
    public class ChallengeBackendChunkGen1 : ChallengeBackendChunk
    {
        protected override uint GetArchivingChunkId()
        {
            return 0x03_043_055u;
        }
    }

    [Chunk(0x3f_001_000u, skippable: true)]
    public class ChallengeBackendChunkGen2 : ChallengeBackendChunk
    {
        protected override uint GetArchivingChunkId()
        {
            return 0x3f_001_000u;
        }
    }

    [Chunk(0x3f_001_001u, skippable: true)]
    public class ChallengeBackendChunkGen3 : ChallengeBackendChunk
    {
        protected override uint GetArchivingChunkId()
        {
            return 0x3f_001_001u;
        }
    }

    [Chunk(0x3f_001_002u, skippable: true)]
    public class ChallengeBackendChunkGen4 : ChallengeBackendChunk
    {
        protected override uint GetArchivingChunkId()
        {
            return 0x3f_001_002u;
        }
    }

    [Chunk(0x3f_001_003u, skippable: true)]
    public class ChallengeBackendChunkGen5 : ChallengeBackendChunk
    {
        protected override uint GetArchivingChunkId()
        {
            return 0x3f_001_003u;
        }
    }
}