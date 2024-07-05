using System;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043011)]
    public class MapChallengeParameterChunk
        : Chunk
    {
        [Property, CustomParserMethod( nameof( ReadCollectorListRef ) )]
        public CollectorListChunk CollectorList { get; set; }

        [Property, CustomParserMethod( nameof( ReadChallengeParametersRef ) )]
        public ChallengeParameters ChallengeParameters { get; set; }

        [Property]
        [Obsolete( "Raw Value, use MapChallengeParameterChunk.Kind instead", false )]
        public uint KindU { get; set; }

        public MapKind Kind { get => ( MapKind )( byte )this.KindU; }

        public CollectorListChunk ReadCollectorListRef(GameBoxReader reader)
        {
            // (instanceIndex) No instance expected to be read.
            if ( reader.ReadUInt32() == uint.MaxValue )
            {
                return null;
            }

            // (classId) Reference to an existing CGameCtnCollectorList instance. Since we cannot
            // maintain a list of instances instantiated from there, we simply return null.
            if ( reader.ReadUInt32() != 0x03_01b_000 )
            {
                reader.Stream.Seek( -4, System.IO.SeekOrigin.Current );
                return null;
            }

            CollectorListChunk result = new CollectorListChunk();
            uint chunkId = reader.ReadUInt32();

            do
            {
                switch ( chunkId )
                {
                    case 0x03_01b_000u:
                    {
                        result.Archive = new CollectorStock[ reader.ReadUInt32() ];

                        for ( uint collectorStockIndex = 0; collectorStockIndex < result.Archive.Length; collectorStockIndex++ )
                        {
                            CollectorStock collectorStock = result.Archive[ collectorStockIndex ] = new CollectorStock();
                            collectorStock.BlockName = reader.ReadLookbackString();
                            collectorStock.Collection = reader.ReadLookbackString();
                            collectorStock.Author = reader.ReadLookbackString();
                            collectorStock.Data = reader.ReadUInt32();
                        }

                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException( $"Unsupported CGameCtnCollectorList chunk: {chunkId:X8}" );
                    }
                }

                chunkId = reader.ReadUInt32();
            }
            while ( chunkId != GameBoxReader.EndMarkerClassId );

            return result;
        }

        public ChallengeParameters ReadChallengeParametersRef(GameBoxReader reader)
        {
            // (instanceIndex) No instance expected to be read.
            if ( reader.ReadUInt32() == uint.MaxValue )
            {
                return null;
            }

            // (classId) Reference to an existing CGameCtnChallengeParameters instance. Since we cannot
            // maintain a list of instances instantiated from there, we simply return null.
            if ( reader.ReadUInt32() != 0x03_05b_000 )
            {
                reader.Stream.Seek( -4, System.IO.SeekOrigin.Current );
                return null;
            }

            ChallengeParameters result = new ChallengeParameters();
            uint chunkId = reader.ReadUInt32();

            do
            {
                switch ( chunkId )
                {
                    case 0x03_05b_000u:
                    {
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        break;
                    }
                    case 0x03_05b_001u:
                    {
                        reader.ReadString(); // ignored
                        reader.ReadString(); // ignored
                        reader.ReadString(); // ignored
                        reader.ReadString(); // ignored
                        break;
                    }
                    case 0x03_05b_002u:
                    {
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadFloat(); // ignored
                        reader.ReadFloat(); // ignored
                        reader.ReadFloat(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        break;
                    }
                    case 0x03_05b_003u:
                    {
                        reader.ReadUInt32(); // ignored
                        reader.ReadFloat(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        break;
                    }
                    case 0x03_05b_004u:
                    {
                        result.BronzeTime = reader.ReadUInt32();
                        result.SilverTime = reader.ReadUInt32();
                        result.GoldTime = reader.ReadUInt32();
                        result.AuthorTime = reader.ReadUInt32();
                        reader.ReadUInt32(); // ignored
                        break;
                    }
                    case 0x03_05b_005u:
                    {
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        reader.ReadUInt32(); // ignored
                        break;
                    }
                    case 0x03_05b_006u:
                    {
                        uint length = reader.ReadUInt32();

                        for ( uint ignoredIndex = 0; ignoredIndex < length; ignoredIndex++ )
                        {
                            reader.ReadUInt32(); // ignored
                        }

                        break;
                    }
                    case 0x03_05b_007u:
                    {
                        result.TimeLimit = reader.ReadUInt32();
                        break;
                    }
                    case 0x03_05b_008u:
                    {
                        result.TimeLimit = reader.ReadUInt32();
                        result.AuthorScore = reader.ReadUInt32();
                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException( $"Unsupported CGameCtnChallengeParameters chunk: {chunkId:X8}" );
                    }
                }

                chunkId = reader.ReadUInt32();
            }
            while ( chunkId != GameBoxReader.EndMarkerClassId );

            return result;
        }
    }

    public class ChallengeParameters
    {
        public uint BronzeTime { get; set; }
        public uint SilverTime { get; set; }
        public uint GoldTime { get; set; }
        public uint AuthorTime { get; set; }
        public uint TimeLimit { get; set; }
        public uint AuthorScore { get; set; }
    }

    public enum MapKind
    : byte
    {
        EndMarker = 0,
        CampaignOld = 1,
        Puzzle = 2,
        Retro = 3,
        TimeAttack = 4,
        Rounds = 5,
        InProgress = 6,
        Campaign = 7,
        Multi = 8,
        Solo = 9,
        Site = 10,
        SoloNadeo = 11,
        MultiNadeo = 12
    }
}
