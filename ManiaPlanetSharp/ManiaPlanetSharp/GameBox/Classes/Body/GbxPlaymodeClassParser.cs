using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxPlaymodeClass
        : GbxBodyClass
    {
        [Obsolete("", false)]
        public uint PlaymodeU { get; set; }

        public GbxTrackType Playmode { get => (GbxTrackType)this.PlaymodeU; }
    }

    public class GbxPlaymodeClassParser
        : GbxBodyClassParser<GbxPlaymodeClass>
    {
        protected override int Chunk => 0x0304301C;

        public override bool Skippable => true;

        protected override GbxPlaymodeClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxPlaymodeClass()
            {
                PlaymodeU = reader.ReadUInt32()
            };
        }
    }
}
