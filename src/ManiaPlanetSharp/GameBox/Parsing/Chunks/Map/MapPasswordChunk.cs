using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043029)]
    public class MapPasswordChunk
        : Chunk
    {
        [Property, Array(2)]
        public ulong[] Password { get; set; }

        [Property]
        public uint CRC { get; set; }
    }
}
