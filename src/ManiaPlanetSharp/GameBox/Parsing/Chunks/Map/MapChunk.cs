using System;
using System.Collections.Generic;
using System.Text;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;

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

        public Block[] ParseBlocks(GameBoxReader reader)
        {
            //The block struct can be parsed by the automatically generated parser
            CustomStructParser<Block> blockParser = ParserFactory.GetCustomStructParser<Block>();

            //This count of blocks that specified the length of the array does not count blocks with empty flags, so we have to read them one by one and check if they are actually counted
            Block[] blocks = new Block[reader.ReadUInt32()];
            for (int i = 0; i < this.Blocks.Length; i++)
            {
                Block block = blockParser.Parse(reader);
                if (block.Flags.HasFlag(BlockFlags.Null))
                {
                    i--; //Ignore parsed block
                }
                else
                {
                    this.Blocks[i] = block;
                }
            }

            return blocks;
        }
    }

    [CustomStruct]
    public class Block
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Name { get; set; }

        [Property]
        public byte Rotation { get; set; }

        [Property]
        public byte X { get; set; }

        [Property]
        public byte Y { get; set; }

        [Property]
        public byte Z { get; set; }

        [Property]
        public uint FlagsU { get; set; }
        public BlockFlags Flags => (BlockFlags)this.FlagsU;
        
        [Property, Condition(nameof(Block.Flags), ConditionOperator.HasFlag, BlockFlags.CustomBlock)]
        public string Author { get; set; }

        [Property, Condition(nameof(Block.Flags), ConditionOperator.HasFlag, BlockFlags.CustomBlock)]
        public Node Skin { get; set; }
        
        [Property, Condition(nameof(Block.Flags), ConditionOperator.HasFlag, BlockFlags.HasBlockParameters)]
        public Node BlockParameters { get; set; }
    }

    [Flags]
    public enum BlockFlags
        : uint
    {
        Null = uint.MaxValue,
        CustomBlock = 0x8000,
        HasBlockParameters = 0x100000
    }
}
