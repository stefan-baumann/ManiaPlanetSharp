using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxGlobalClipClass
        : Node
    {
        public Node GlobalClip { get; set; }
    }

    public class GbxGlobalClipClassParser
        : ClassParser<GbxGlobalClipClass>
    {
        protected override int ChunkId => 0x03043026;

        protected override GbxGlobalClipClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxGlobalClipClass()
            {
                GlobalClip = reader.ReadNodeReference()
            };
        }
    }
}
