using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092014, Skippable = true)]
    public class GhostUnknown6Chunk
        : Chunk
    {
        [Property]
        public uint Unknown1 { get; set; }
    }
}
