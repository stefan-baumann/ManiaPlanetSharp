using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0305B00A, Skippable = true)]
    public class MapTimelimitTimeChunk
        : Chunk
    {
        [Property]
        public uint Unknown { get; set; }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.BronzeTime instead", false)]
        public uint BronzeTimeU { get; set; }

        public TimeSpan BronzeTime { get => TimeSpan.FromMilliseconds(this.BronzeTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.SilverTime instead", false)]
        public uint SilverTimeU { get; set; }

        public TimeSpan SilverTime { get => TimeSpan.FromMilliseconds(this.SilverTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.GoldTime instead", false)]
        public uint GoldTimeU { get; set; }

        public TimeSpan GoldTime { get => TimeSpan.FromMilliseconds(this.GoldTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.AuthorTime instead", false)]
        public uint AuthorTimeU { get; set; }

        public TimeSpan AuthorTime { get => TimeSpan.FromMilliseconds(this.AuthorTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.TimeLimit instead", false)]
        public uint TimeLimitU { get; set; }

        public TimeSpan TimeLimit { get => TimeSpan.FromMilliseconds(this.TimeLimitU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.AuthorScore instead", false)]
        public uint AuthorScoreU { get; set; }

        public TimeSpan AuthorScore { get => TimeSpan.FromMilliseconds(this.AuthorScoreU); }
    }
}
