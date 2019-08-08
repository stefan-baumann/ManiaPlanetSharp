using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorLightmapCacheIdClass
        : GbxClass
    {
        public ulong FileTime { get; set; }
    }

    public class GbxCollectorLightmapCacheIdParser
        : GbxClassParser<GbxCollectorLightmapCacheIdClass>
    {
        protected override int ChunkId => 0x2E001006;

        protected override GbxCollectorLightmapCacheIdClass ParseChunkInternal(GbxReader chunk)
        {
            return new GbxCollectorLightmapCacheIdClass()
            {
                FileTime = chunk.ReadUInt64()
            };
        }
    }
}
