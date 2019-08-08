using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxReplayCommunityClass
        : GbxClass
    {
        public string XmlString { get; set; }
    }

    public class GbxReplayCommunityClassParser
        : GbxClassParser<GbxReplayCommunityClass>
    {
        protected override int ChunkId => 0x03093001;

        protected override GbxReplayCommunityClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxReplayCommunityClass()
            {
                XmlString = reader.ReadString()
            };
        }
    }
}
