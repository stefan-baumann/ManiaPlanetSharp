using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002018)]
    public class ObjectUsabilityChunk
        : Chunk
    {
        [Property]
        public int Version { get; set; }

        [Property]
        public bool IsUsable { get; set; }
    }
}
