using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E00100C)]
    public class CollectorNameChunk
        : Chunk
    {
        [Property]
        public string Name { get; set; }
    }
}
