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

        public TimeSpan? TimeLimit { get => this.TimeLimitU != uint.MaxValue ? (TimeSpan?)TimeSpan.FromMilliseconds(this.TimeLimitU) : null; }

        [Property]
        [Obsolete("Raw Value, use MapTimelimitChunk.AuthorTime instead", false)]
        public uint AuthorTimeU { get; set; }

        public TimeSpan? AuthorTime { get => this.AuthorTimeU != uint.MaxValue ? (TimeSpan?)TimeSpan.FromMilliseconds(this.AuthorTimeU) : null; }
    }
}
