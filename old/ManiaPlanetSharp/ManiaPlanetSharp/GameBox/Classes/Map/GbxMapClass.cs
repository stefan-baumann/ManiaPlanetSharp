using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxMapClass
        : Node
    {
        public string Uid { get; set; }
        public string Environment { get; set; }
        public string Author { get; set; }
        public string MapName { get; set; }
        public string TimeOfDay { get; set; }
        public string DecorationEnvironment { get; set; }
        public string DecorationEnvironmentAuthor { get; set; }
        public uint SizeX { get; set; }
        public uint SizeY { get; set; }
        public uint SizeZ { get; set; }
        public bool NeedsUnlock { get; set; }
        public uint Version { get; set; }
        public uint BlockCount { get; set; }
        public Block[] Blocks { get; set; }

    }

    public class Block
    {
        public string Name { get; set; }
        public byte Rotation { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte Z { get; set; }
        public uint Flags { get; set; }

        //Custom Blocks Only
        public string Author { get; set; }
        public Node Skin { get; set; }
         
        public Node BlockParameters { get; set; }
    }

    public class GbxMapClassParser
        : ClassParser<GbxMapClass>
    {
        protected override int ChunkId => 0x0304301F;

        protected override GbxMapClass ParseChunkInternal(GameBoxReader reader)
        {
            GbxMapClass map = new GbxMapClass();

            //Parse Metadata
            map.Uid = reader.ReadLookbackString();
            map.Environment = reader.ReadLookbackString();
            map.Author = reader.ReadLookbackString();
            map.MapName = reader.ReadString();
            map.TimeOfDay = reader.ReadLookbackString();
            map.DecorationEnvironment = reader.ReadLookbackString();
            map.DecorationEnvironmentAuthor = reader.ReadLookbackString();
            map.SizeX = reader.ReadUInt32();
            map.SizeY = reader.ReadUInt32();
            map.SizeZ = reader.ReadUInt32();
            map.NeedsUnlock = reader.ReadBool();

            //if chunkId != 03043013
            map.Version = reader.ReadUInt32();
            map.BlockCount = reader.ReadUInt32();
            //Debug.WriteLine($"        Blocks: {map.BlockCount}");
            map.Blocks = new Block[map.BlockCount];
            for (int i = 0; i < map.BlockCount; i++)
            {
                Block block = new Block();
                block.Name = reader.ReadLookbackString();
                block.Rotation = reader.ReadByte();
                block.X = reader.ReadByte();
                block.Y = reader.ReadByte();
                block.Z = reader.ReadByte();
                block.Flags = map.Version == 0 ? reader.ReadUInt16() : map.Version > 0 ? reader.ReadUInt32() : 0;

                //Debug.WriteLine($"        Block #{i} ({block.Name} at [{block.X}, {block.Y}, {block.Z}])");
                if (block.Flags == uint.MaxValue)
                {
                    i--; //These blocks are not counted for the block count
                    continue;
                }
                if ((block.Flags & 0x8000) != 0) //Custom Block
                {
                    block.Author = reader.ReadLookbackString();
                    block.Skin = reader.ReadNodeReference();
                }
                if ((block.Flags & 0x100000) != 0)
                {
                    block.BlockParameters = reader.ReadNodeReference();
                }

                map.Blocks[i] = block;
            }

            //There might be additional blocks with flags uint.MaxValue after

            return map;
        }
    }
}
