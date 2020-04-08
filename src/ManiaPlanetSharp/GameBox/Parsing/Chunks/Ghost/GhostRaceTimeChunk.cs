using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092005, Skippable = true)]
    public class GhostRaceTimeChunk
        : Chunk
    {
        [Property]
        public uint RaceTimeU { get; set; }

        public TimeSpan RaceTime { get => TimeSpan.FromMilliseconds(this.RaceTimeU); }
    }
}
