using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.Other
{
    //[Chunk(0x2E006000)]
    public class PhysicalModelChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node Unknown1 { get; set; }
    }
}
