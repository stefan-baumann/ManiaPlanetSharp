using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E001007)]
    public class CollectorCatalogChunk
        : Chunk
    {
        [Property]
        public bool IsInternal { get; set; }

        [Property]
        public uint Unused1 { get; set; }

        [Property]
        public uint CatalogPosition { get; set; }

        [Property]
        public uint Unused2 { get; set; }

        [Property]
        public uint Unused3 { get; set; }

        [Property]
        public uint Unused4 { get; set; }
    }
}
