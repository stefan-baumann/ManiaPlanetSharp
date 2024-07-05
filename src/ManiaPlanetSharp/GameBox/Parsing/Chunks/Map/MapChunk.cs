using System;
using ManiaPlanetSharp.GameBox.Types;
using SharpCompress.Readers;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0304301F)]
    public class MapChunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Uid { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Environment { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Author { get; set; }

        [Property]
        public string Name { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string TimeOfDay { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string DecorationEnvironment { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string DecorationEnvironmentAuthor { get; set; }

        [Property]
        public Size3D Size { get; set; }

        [Property]
        public bool NeedsUnlock { get; set; }

        [Property]
        public uint Version { get; set; }

        [Property, CustomParserMethod(nameof(MapChunk.ParseBlocks))]
        public Block[] Blocks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public Block[] ParseBlocks(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var start = reader.Stream.Position;

            //This count of blocks that specified the length of the array does not count blocks with empty flags, so we have to read them one by one and check if they are actually counted
            Block[] blocks = new Block[reader.ReadUInt32()];
            try
            {
                for (int i = 0; i < blocks.Length; i++)
                {
                    Block block = blocks[i] = new Block();

                    block.Name = reader.ReadLookbackString();
                    block.Rotation = reader.ReadByte();
                    block.X = reader.ReadByte();
                    block.Y = reader.ReadByte();
                    block.Z = reader.ReadByte();

                    if ( Version > 0 )
                    {
                        block.FlagsU = reader.ReadUInt32();
                    }
                    else
                    {
                        block.FlagsU = reader.ReadUInt16();
                    }

                    if ( block.Flags.HasFlag( BlockFlags.HasSkin ) )
                    {
                        block.Author = reader.ReadLookbackString();
                        block.Skin = ReadSkinRef( reader );
                    }

                    if ( Version <= 1 )
                    {
                        continue;
                    }
                    else if ( Version >= 6 )
                    {
                        block.X--;
                        block.Z--;
                    }

                    if ( block.Flags.HasFlag( BlockFlags.HasPhyCharSpecialProperty ) )
                    {
                        throw new NotSupportedException( "Cannot read block - Node references are not properly supported in ManiaPlanetSharp" );
                    }

                    if ( block.Flags.HasFlag( BlockFlags.HasWaypointSpecialProperty ) )
                    {
                        block.WaypointSpecialProperty = ReadWaypointSpecialPropertyRef( reader );
                    }

                    if ( block.Flags.HasFlag( BlockFlags.HasSquareCardEventIds ) )
                    {
                        block.SquareCardEventIds = new SquareCardEventIds[reader.ReadUInt32()];

                        for ( uint squareCardEventIdIndex = 0; squareCardEventIdIndex < block.SquareCardEventIds.Length; squareCardEventIdIndex++ )
                        {
                            var squareCardEventId = block.SquareCardEventIds[ squareCardEventIdIndex ];
                            squareCardEventId.Unknown01 = reader.ReadInt32();
                            squareCardEventId.Unknown02 = reader.ReadInt32();
                            squareCardEventId.Unknown03 = new (string, string, string)[reader.ReadUInt32()];

                            for ( uint eventIdIndex = 0; eventIdIndex < squareCardEventId.Unknown03.Length; eventIdIndex++ )
                            {
                                squareCardEventId.Unknown03[ eventIdIndex ] =
                                (
                                    reader.ReadLookbackString(),
                                    reader.ReadLookbackString(),
                                    reader.ReadLookbackString()
                                );
                            }
                        }
                    }

                    if ( block.Flags.HasFlag( BlockFlags.HasDecal ) )
                    {
                        block.DecalId = reader.ReadLookbackString();
                        block.DecalIntensity = reader.ReadInt32();
                        block.DecalVariant = reader.ReadInt32();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("GameBox Parser: Could not successfully parse map blocks.");
                reader.Stream.Position = start;
                return null;
            }

            return blocks;
        }

        // This function takes care of the nod reference, which is expected to be a CGameCtnBlockSkin instance.
        // In the case of referencing the already instantiated CGameCtnBlockSkin instance, it simply returns null
        // as we cannot maintain a list of instantiated instances from there. This approach makes reading
        // consistent at the cost of a less accurate block representation (lack of block skin information).
        private GameCtnBlockSkin ReadSkinRef( GameBoxReader reader )
        {
            // (instanceIndex) No instance expected to be read.
            if ( reader.ReadUInt32() == uint.MaxValue )
            {
                return null;
            }

            // (classId) Reference to an existing CGameCtnBlockSkin instance. Since we cannot
            // maintain a list of instances instantiated from there, we simply return null.
            if ( reader.ReadUInt32() != 0x03_059_000u )
            {
                reader.Stream.Seek( -4, System.IO.SeekOrigin.Current );
                return null;
            }

            GameCtnBlockSkin result = new GameCtnBlockSkin();
            uint chunkId = reader.ReadUInt32();

            do
            {
                switch ( chunkId )
                {
                    case 0x03_059_000u:
                    {
                        result.Text = reader.ReadString();
                        reader.ReadString(); // ignored string
                        break;
                    }
                    case 0x03_059_001u:
                    {
                        result.Text = reader.ReadString();
                        result.PackDesc = reader.ReadFileReference();
                        break;
                    }
                    case 0x03_059_002u:
                    {
                        result.Text = reader.ReadString();
                        result.PackDesc = reader.ReadFileReference();
                        result.ParentPackDesc = reader.ReadFileReference();
                        break;
                    }
                    case 0x03_059_003u:
                    {
                        reader.ReadUInt32(); // Version, at the time of implementation - not used
                        result.ForegroundPackDesc = reader.ReadFileReference();
                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException( $"Unsupported CGameCtnBlockSkin chunk: {chunkId:X8}" );
                    }
                }

                chunkId = reader.ReadUInt32();
            }
            while ( chunkId != GameBoxReader.EndMarkerClassId );

            return result;
        }

        private WaypointSpecialPropertyChunk ReadWaypointSpecialPropertyRef(GameBoxReader reader)
        {
            // (instanceIndex) No instance expected to be read.
            if ( reader.ReadUInt32() == uint.MaxValue )
            {
                return null;
            }

            // (classId) Reference to an existing CGameWaypointSpecialProperty instance. Since we cannot
            // maintain a list of instances instantiated from there, we simply return null.
            if ( reader.ReadUInt32() != 0x2e_009_000u )
            {
                reader.Stream.Seek( -4, System.IO.SeekOrigin.Current );
                return null;
            }

            WaypointSpecialPropertyChunk result = new WaypointSpecialPropertyChunk();
            uint chunkId = reader.ReadUInt32();

            do
            {
                switch ( chunkId )
                {
                    case 0x2e_009_000u:
                    {
                        result.Version = reader.ReadUInt32();

                        switch ( result.Version )
                        {
                            case 1:
                            {
                                result.Spawn = reader.ReadUInt32();
                                result.Order = reader.ReadUInt32();
                                break;
                            }
                            case 2:
                            {
                                result.Tag = reader.ReadString();
                                result.Order = reader.ReadUInt32();
                                break;
                            }
                        }

                        break;
                    }
                    case 0x2e_009_001u: // (skippable)
                    {
                        if ( reader.ReadUInt32() != GameBoxReader.SkipMarker )
                        {
                            throw new Exception( "CGameWaypointSpecialProperty 0x001 chunk is missing PIKS" );
                        }

                        reader.Skip( reader.ReadInt32() );
                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException( $"Unsupported CGameWaypointSpecialProperty chunk: {chunkId:X8}" );
                    }
                }

                chunkId = reader.ReadUInt32();
            }
            while ( chunkId != GameBoxReader.EndMarkerClassId );

            return result;
        }
    }

    public class SquareCardEventIds
    {
        public int Unknown01 { get; set; }
        public int Unknown02 { get; set; }
        public (string, string, string)[] Unknown03 { get; set; }
    }

    public class Block
    {
        public string Name { get; set; }
        public byte Rotation { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte Z { get; set; }
        public uint FlagsU { get; set; }
        public BlockFlags Flags => (BlockFlags)this.FlagsU;
        public string Author { get; set; }
        public GameCtnBlockSkin Skin { get; set; }
        public string DecalId { get; set; }
        public int DecalIntensity { get; set; }
        public int DecalVariant { get; set; }
        public SquareCardEventIds[] SquareCardEventIds { get; set; }
        public Node PhyCharSpecialProperty { get; set; }
        public Node WaypointSpecialProperty { get; set; }
    }

    [Flags]
    public enum BlockFlags
        : uint
    {
        HasSkin = 1 << 15,
        HasDecal = 1 << 17,
        HasSquareCardEventIds = 1 << 18,
        HasPhyCharSpecialProperty = 1 << 19,
        HasWaypointSpecialProperty = 1 << 20
    }
}
