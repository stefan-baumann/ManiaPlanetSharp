using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxLightmapCacheId
        : GbxClass
    {
        public ulong FileTime { get; set; }
    }

    public class GbxLightmapCacheIdParser
        : GbxClassParser<GbxLightmapCacheId>
    {
        protected override int ChunkId => 0x2E001006;

        protected override GbxLightmapCacheId ParseChunkInternal(GbxReader chunk)
        {
            return new GbxLightmapCacheId()
            {
                FileTime = chunk.ReadUInt64()
            };
        }
    }
}
