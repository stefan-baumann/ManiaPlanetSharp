using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectType
        : GbxClass
    {
        public uint ItemTypeId { get; set; }
    }

    public class GbxObjectTypeParser
        : GbxClassParser<GbxObjectType>
    {
        protected override int ChunkId => 0x2E002000;

        protected override GbxObjectType ParseChunkInternal(GbxReader chunk)
        {
            return new GbxObjectType()
            {
                ItemTypeId = chunk.ReadUInt32()
            };
        }
    }
}
