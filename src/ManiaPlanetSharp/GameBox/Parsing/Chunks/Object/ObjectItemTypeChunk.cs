using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002000)]
    public class ObjectItemTypeChunk
        : Chunk
    {
        [Property]
        public uint ItemTypeU { get; set; }

        public ObjectType ItemType => (ObjectType)this.ItemTypeU;
    }
}
