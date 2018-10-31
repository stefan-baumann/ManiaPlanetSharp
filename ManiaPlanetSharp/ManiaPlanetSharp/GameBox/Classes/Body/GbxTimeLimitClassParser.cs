using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxTimeLimitClass
        : GbxBodyClass
    {
        [Obsolete("", false)]
        public uint TimeLimitU { get; set; }
        public TimeSpan TimeLimit { get => TimeSpan.FromMilliseconds(this.TimeLimitU); }
        [Obsolete("", false)]
        public uint AuthorScoreU { get; set; }
        public TimeSpan AuthorScore { get => TimeSpan.FromMilliseconds(this.AuthorScoreU); }
    }

    public class GbxTimeLimitClassParser
        : GbxBodyClassParser<GbxTimeLimitClass>
    {
        protected override int Chunk => 0x0305B008;

        public override bool Skippable => true;

        protected override GbxTimeLimitClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxTimeLimitClass()
            {
                TimeLimitU = reader.ReadUInt32(),
                AuthorScoreU = reader.ReadUInt32()
            };
        }
    }
}
