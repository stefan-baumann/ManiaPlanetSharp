using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorLightmapCacheIdClass
        : Node
    {
        public ulong FileTime { get; set; }
    }

    public class GbxCollectorLightmapCacheIdParser
        : ClassParser<GbxCollectorLightmapCacheIdClass>
    {
        protected override int ChunkId => 0x2E001006;

        protected override GbxCollectorLightmapCacheIdClass ParseChunkInternal(GameBoxReader chunk)
        {
            return new GbxCollectorLightmapCacheIdClass()
            {
                FileTime = chunk.ReadUInt64()
            };
        }
    }
}
