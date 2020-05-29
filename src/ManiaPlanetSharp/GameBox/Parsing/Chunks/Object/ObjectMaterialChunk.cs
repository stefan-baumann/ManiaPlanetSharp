using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E00200B)]
    public class ObjectMaterialChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node StemMaterial { get; set; }

        [Property(SpecialPropertyType.NodeReference)]
        public Node StemBumpMaterial { get; set; }
    }
}
