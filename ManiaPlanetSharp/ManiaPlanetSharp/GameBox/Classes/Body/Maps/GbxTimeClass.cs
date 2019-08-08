using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox
{
    public class GbxTimeClass
        : GbxClass
    {
        [Obsolete("Raw Value, use GbxTimeClass.BronzeTime instead", false)]
        public uint BronzeTimeU { get; set; }
        public TimeSpan BronzeTime { get => TimeSpan.FromMilliseconds(this.BronzeTimeU); }
        [Obsolete("Raw Value, use GbxTimeClass.SilverTime instead", false)]
        public uint SilverTimeU { get; set; }
        public TimeSpan SilverTime { get => TimeSpan.FromMilliseconds(this.SilverTimeU); }
        [Obsolete("Raw Value, use GbxTimeClass.GoldTime instead", false)]
        public uint GoldTimeU { get; set; }
        public TimeSpan GoldTime { get => TimeSpan.FromMilliseconds(this.GoldTimeU); }
        [Obsolete("Raw Value, use GbxTimeClass.AuthorTime instead", false)]
        public uint AuthorTimeU { get; set; }
        public TimeSpan AuthorTime { get => TimeSpan.FromMilliseconds(this.AuthorTimeU); }

        public uint Ignored { get; set; }
    }

    public class GbxTimeClassParser
        : GbxClassParser<GbxTimeClass>
    {
        protected override int ChunkId => 0x0305B004;

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
