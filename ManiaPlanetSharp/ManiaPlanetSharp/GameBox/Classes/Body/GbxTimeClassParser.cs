using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxTimeClass
        : GbxBodyClass
    {
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

        public uint Ignored { get; set; }
    }

    public class GbxTimeClassParser
        : GbxBodyClassParser<GbxTimeClass>
    {
        protected override int Chunk => 0x0305B004;

        public override bool Skippable => true;

        protected override GbxTimeClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxTimeClass()
            {
                BronzeTimeU = reader.ReadUInt32(),
                SilverTimeU = reader.ReadUInt32(),
                GoldTimeU = reader.ReadUInt32(),
                AuthorTimeU = reader.ReadUInt32(),
                Ignored = reader.ReadUInt32()
            };
        }
    }
}
