using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxGlobalClipClass
        : GbxBodyClass
    {
        public GbxNode GlobalClip { get; set; }
    }

    public class GbxGlobalClipClassParser
        : GbxBodyClassParser<GbxGlobalClipClass>
    {
        protected override int Chunk => 0x03043026;

        protected override GbxGlobalClipClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxGlobalClipClass()
            {
                GlobalClip = reader.ReadNodeReference()
            };
        }
    }
}
