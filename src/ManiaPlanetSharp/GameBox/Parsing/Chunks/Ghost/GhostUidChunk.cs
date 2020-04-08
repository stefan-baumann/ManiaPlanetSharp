using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0309200E)]
    public class GhostUidChunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Uid { get; set; }
    }
}
