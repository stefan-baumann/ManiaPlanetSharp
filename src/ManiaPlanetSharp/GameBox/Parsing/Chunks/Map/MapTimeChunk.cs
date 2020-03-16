using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0305B004, Skippable = true)]
    public class MapTimeChunk
        : Chunk
    {
        [Property]
        [Obsolete("Raw Value, use GbxTimeClass.BronzeTime instead", false)]
        public uint BronzeTimeU { get; set; }

        public TimeSpan BronzeTime { get => TimeSpan.FromMilliseconds(this.BronzeTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeClass.SilverTime instead", false)]
        public uint SilverTimeU { get; set; }

        public TimeSpan SilverTime { get => TimeSpan.FromMilliseconds(this.SilverTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeClass.GoldTime instead", false)]
        public uint GoldTimeU { get; set; }

        public TimeSpan GoldTime { get => TimeSpan.FromMilliseconds(this.GoldTimeU); }

        [Property]
        [Obsolete("Raw Value, use GbxTimeClass.AuthorTime instead", false)]
        public uint AuthorTimeU { get; set; }

        public TimeSpan AuthorTime { get => TimeSpan.FromMilliseconds(this.AuthorTimeU); }

        [Property]
        public uint Ignored { get; set; }
    }
}
