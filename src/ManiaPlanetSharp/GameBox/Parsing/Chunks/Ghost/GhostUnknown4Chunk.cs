using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092012)]
    public class GhostUnknown4Chunk
        : Chunk
    {
        [Property]
        public uint Ignored { get; set; }

        [Property, Array(2)]
        public ulong[] Unknown { get; set; }
    }
}
