using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092008, Skippable = true)]
    public class GhostRespawnCountChunk
        : Chunk
    {
        [Property]
        public uint RespawnCount { get; set; }
    }
}
