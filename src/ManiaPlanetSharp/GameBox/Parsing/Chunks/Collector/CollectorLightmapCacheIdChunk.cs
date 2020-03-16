using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E001006)]
    public class CollectorLightmapCacheIdChunk
        : Chunk
    {
        [Property]
        public ulong FileTime { get; set; }
    }
}
