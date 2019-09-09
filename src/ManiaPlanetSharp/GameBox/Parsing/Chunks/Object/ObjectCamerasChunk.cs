using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E002009)]
    public class ObjectCamerasChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property(SpecialPropertyType.NodeReference), Array]
        public Node[] Cameras { get; set; }
    }
}
