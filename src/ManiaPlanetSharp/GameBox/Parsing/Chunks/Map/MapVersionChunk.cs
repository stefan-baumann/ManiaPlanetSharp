using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x3043004)]
    public class MapVersionChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }
    }
}
