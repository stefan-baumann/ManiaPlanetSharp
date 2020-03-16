using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043018, Skippable = true)]
    public class LapCountChunk
    {
        [Property]
        public bool Unused { get; set; }

        [Property]
        public uint LapCount { get; set; }
    }
}
