using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E002008)]
    public class ObjectNadeoSkinFileChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference), Array]
        public Node[] Files { get; set; }
    }
}
