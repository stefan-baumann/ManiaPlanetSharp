using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E00100D)]
    public class CollectorDescriptionChunk
        : Chunk
    {
        [Property]
        public string Description { get; set; }
    }
}
