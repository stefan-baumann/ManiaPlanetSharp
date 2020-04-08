using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x03093015)]
    public class ReplayUnknown2Chunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node Node { get; set; }
    }
}
