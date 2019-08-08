using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxReplayCommunityClass
        : GbxBodyClass
    {
        public string XmlString { get; set; }
    }

    public class GbxReplayCommunityClassParser
        : GbxBodyClassParser<GbxReplayCommunityClass>
    {
        protected override int Chunk => 0x03093001;

        protected override GbxReplayCommunityClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxReplayCommunityClass()
            {
                XmlString = reader.ReadString()
            };
        }
    }
}
