using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox
{
    public class GbxTimeLimitTimeClass
        : GbxBodyClass
    {
        public uint Unknown { get; set; }

        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.BronzeTime instead", false)]
        public uint BronzeTimeU { get; set; }
        public TimeSpan BronzeTime { get => TimeSpan.FromMilliseconds(this.BronzeTimeU); }
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.SilverTime instead", false)]
        public uint SilverTimeU { get; set; }
        public TimeSpan SilverTime { get => TimeSpan.FromMilliseconds(this.SilverTimeU); }
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.GoldTime instead", false)]
        public uint GoldTimeU { get; set; }
        public TimeSpan GoldTime { get => TimeSpan.FromMilliseconds(this.GoldTimeU); }
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.AuthorTime instead", false)]
        public uint AuthorTimeU { get; set; }
        public TimeSpan AuthorTime { get => TimeSpan.FromMilliseconds(this.AuthorTimeU); }
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.TimeLimit instead", false)]
        public uint TimeLimitU { get; set; }
        public TimeSpan TimeLimit { get => TimeSpan.FromMilliseconds(this.TimeLimitU); }
        [Obsolete("Raw Value, use GbxTimeLimitTimeClass.AuthorScore instead", false)]
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
