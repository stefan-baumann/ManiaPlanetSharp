using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxGlobalClipClass
        : GbxClass
    {
        public GbxNode GlobalClip { get; set; }
    }

    public class GbxGlobalClipClassParser
        : GbxClassParser<GbxGlobalClipClass>
    {
        protected override int ChunkId => 0x03043026;

        protected override GbxGlobalClipClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxGlobalClipClass()
            {
                GlobalClip = reader.ReadNodeReference()
            };
        }
    }
}
