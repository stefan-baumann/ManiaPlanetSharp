using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E002014)]
    public class ObjectBaseAttributesChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node BaseAttributes { get; set; }
    }
}
