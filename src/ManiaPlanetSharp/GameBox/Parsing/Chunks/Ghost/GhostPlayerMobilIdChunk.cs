using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(03092015)]
    public class GhostPlayerMobilIdChunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string PlayerMobilId { get; set; }
    }
}
