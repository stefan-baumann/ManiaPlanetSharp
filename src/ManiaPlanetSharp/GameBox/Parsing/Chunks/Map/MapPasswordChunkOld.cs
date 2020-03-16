using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043014, Skippable = true)]
    public class MapPasswordChunkOld
        : Chunk
    {
        [Property]
        public uint Unused { get; set; }

        [Property]
        public string Password { get; set; }
    }
}
