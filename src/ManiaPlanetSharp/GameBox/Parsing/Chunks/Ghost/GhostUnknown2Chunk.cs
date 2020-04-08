using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0309200C)]
    public class GhostUnknown2Chunk
        : Chunk
    {
        [Property]
        public uint Ignored { get; set; }
    }
}
