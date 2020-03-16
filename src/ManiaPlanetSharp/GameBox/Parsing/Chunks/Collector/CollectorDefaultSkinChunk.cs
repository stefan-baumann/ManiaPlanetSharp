using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E00100F)]
    public class CollectorDefaultSkinChunk
        : Chunk
    {
        [Property]
        public string DefaultSkinName { get; set; }
    }
}
