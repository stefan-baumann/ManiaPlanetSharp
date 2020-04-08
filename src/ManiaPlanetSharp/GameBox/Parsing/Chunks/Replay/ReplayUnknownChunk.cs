using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093007, Skippable = true)]
    public class ReplayUnknownChunk
        : Chunk
    {
        [Property]
        public uint Unknown { get; set; }
    }
}
