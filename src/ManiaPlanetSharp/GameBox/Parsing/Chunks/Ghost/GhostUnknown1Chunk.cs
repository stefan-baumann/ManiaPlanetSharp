using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0309200B, Skippable = true)]
    public class GhostUnknown1Chunk
        : Chunk
    {
        [Property, Array]
        public ulong[] Unknown { get; set; }
    }
}
