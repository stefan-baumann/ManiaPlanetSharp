using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxLightmapCacheId
        : GbxHeaderClass
    {
        public ulong FileTime { get; set; }
    }

    public class GbxLightmapCacheIdParser
        : GbxHeaderClassParser<GbxLightmapCacheId>
    {
        protected override int Chunk => 0x2E001006;

        public override GbxLightmapCacheId ParseChunk(GbxReader chunk)
        {
            return new GbxLightmapCacheId()
            {
                FileTime = chunk.ReadUInt64()
            };
        }
    }
}
