using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x03043026)]
    public class MapGlobalClipChunk
        : Chunk
    {
        [Property]
        public Node GlobalClip { get; set; }
    }
}
