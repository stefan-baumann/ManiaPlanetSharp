using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002017)]
    public class ObjectAnchorChunk
        : Chunk
    {
        [Property]
        public int Version { get; set; }

        [Property]
        public bool IsFreelyAnchorable { get; set; }
    }
}
