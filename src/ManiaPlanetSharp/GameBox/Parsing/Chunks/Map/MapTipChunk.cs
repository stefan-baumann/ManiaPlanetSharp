using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0305B001)]
    public class MapTipChunk
        : Chunk
    {
        [Property]
        public string Tip1 { get; set; }

        [Property]
        public string Tip2 { get; set; }

        [Property]
        public string Tip3 { get; set; }

        [Property]
        public string Tip4 { get; set; }
    }
}
