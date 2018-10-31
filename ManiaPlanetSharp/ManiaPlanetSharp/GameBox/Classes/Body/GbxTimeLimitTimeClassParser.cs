using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxTimeLimitTimeClass
        : GbxBodyClass
    {
        public uint Unknown { get; set; }

        [Obsolete("", false)]
        public uint BronzeTimeU { get; set; }
        public TimeSpan BronzeTime { get => TimeSpan.FromMilliseconds(this.BronzeTimeU); }
        [Obsolete("", false)]
        public uint SilverTimeU { get; set; }
        public TimeSpan SilverTime { get => TimeSpan.FromMilliseconds(this.SilverTimeU); }
        [Obsolete("", false)]
        public uint GoldTimeU { get; set; }
        public TimeSpan GoldTime { get => TimeSpan.FromMilliseconds(this.GoldTimeU); }
        [Obsolete("", false)]
        public uint AuthorTimeU { get; set; }
        public TimeSpan AuthorTime { get => TimeSpan.FromMilliseconds(this.AuthorTimeU); }
        [Obsolete("", false)]
        public uint TimeLimitU { get; set; }
        public TimeSpan TimeLimit { get => TimeSpan.FromMilliseconds(this.TimeLimitU); }
        [Obsolete("", false)]
        public uint AuthorScoreU { get; set; }
        public TimeSpan AuthorScore { get => TimeSpan.FromMilliseconds(this.AuthorScoreU); }
    }

    public class GbxTimeLimitTimeClassParser
        : GbxBodyClassParser<GbxTimeLimitTimeClass>
    {
        protected override int Chunk => 0x0305B00A;

        public override bool Skippable => true;

        protected override GbxTimeLimitTimeClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxTimeLimitTimeClass()
            {
                Unknown = reader.ReadUInt32(),
                BronzeTimeU = reader.ReadUInt32(),
                SilverTimeU = reader.ReadUInt32(),
                GoldTimeU = reader.ReadUInt32(),
                AuthorTimeU = reader.ReadUInt32(),
                TimeLimitU = reader.ReadUInt32(),
                AuthorScoreU = reader.ReadUInt32()
            };
        }
    }
}
