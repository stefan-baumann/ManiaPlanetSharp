using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxEmbeddedItemsClass
        : GbxBodyClass
    {
        public uint Version { get; set; }
        public uint Unknown { get; set; }
        public uint ChunkSize { get; set; }
        public uint ItemCount { get; set; }
        //public uint ZipSize { get; set; }
        public byte[] ZipFile { get; set; }
    }

    public class GbxEmbeddedItemsClassParser
        : GbxBodyClassParser<GbxEmbeddedItemsClass>
    {
        protected override int Chunk => 0x03043054;

        public override bool Skippable => true;

        protected override GbxEmbeddedItemsClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxEmbeddedItemsClass()
            {
                Version = reader.ReadUInt32(),
                Unknown = reader.ReadUInt32(),
                ChunkSize = reader.ReadUInt32(),
                ItemCount = reader.ReadUInt32(),
                ZipFile = reader.ReadRaw((int)reader.ReadUInt32())
            };
        }
    }
}
