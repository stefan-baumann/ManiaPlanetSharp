using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxLightmapCacheIdClass
        : GbxClass
    {
        public ulong FileTime { get; set; }
    }

    public class GbxLightmapCacheIdParser
        : GbxClassParser<GbxLightmapCacheIdClass>
    {
        protected override int ChunkId => 0x2E001006;

        protected override GbxLightmapCacheIdClass ParseChunkInternal(GbxReader chunk)
        {
            return new GbxLightmapCacheIdClass()
            {
                FileTime = chunk.ReadUInt64()
            };
        }
    }
}
