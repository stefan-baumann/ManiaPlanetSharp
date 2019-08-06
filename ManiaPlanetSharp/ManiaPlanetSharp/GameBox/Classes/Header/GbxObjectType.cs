using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectType
        : GbxHeaderClass
    {
        public uint ItemTypeId { get; set; }
    }

    public class GbxObjectTypeParser
        : GbxHeaderClassParser<GbxObjectType>
    {
        protected override int Chunk => 0;

        public override GbxObjectType ParseChunk(GbxReader chunk)
        {
            return new GbxObjectType()
            {
                ItemTypeId = chunk.ReadUInt32()
            };
        }
    }
}
