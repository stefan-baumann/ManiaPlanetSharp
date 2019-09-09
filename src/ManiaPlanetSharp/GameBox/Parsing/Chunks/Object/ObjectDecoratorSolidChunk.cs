using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E00200A)]
    public class ObjectDecoratorSolidChunk
        : Chunk
    {
        [Property]
        public Node DecoratorSolid { get; set; }
    }
}
