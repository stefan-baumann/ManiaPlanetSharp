using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0305B008, Skippable = true)]
    public class MapTimelimitChunk
        : Chunk
    {
        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitClass.TimeLimit instead", false)]
        public uint TimeLimitU { get; set; }

        public TimeSpan TimeLimit { get => TimeSpan.FromMilliseconds(this.TimeLimitU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitClass.AuthorScore instead", false)]
        public uint AuthorScoreU { get; set; }

        public TimeSpan AuthorScore { get => TimeSpan.FromMilliseconds(this.AuthorScoreU); }
    }
}
