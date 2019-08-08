using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxPlaymodeClass
        : Node
    {
        [Obsolete("Raw Value, use GbxPlaymodeClass.Playmode instead", false)]
        public uint PlaymodeU { get; set; }

        public GbxTrackType Playmode { get => (GbxTrackType)this.PlaymodeU; }
    }

    public class GbxPlaymodeClassParser
        : ClassParser<GbxPlaymodeClass>
    {
        protected override int ChunkId => 0x0304301C;

        public override bool Skippable => true;

        protected override GbxPlaymodeClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxPlaymodeClass()
            {
                PlaymodeU = reader.ReadUInt32()
            };
        }
    }
}
