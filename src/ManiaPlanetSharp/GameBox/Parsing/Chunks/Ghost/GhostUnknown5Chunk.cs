using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092013, Skippable = true)]
    public class GhostUnknown5Chunk
        : Chunk
    {
        [Property]
        public uint Unknown1 { get; set; }

        [Property]
        public uint Unknown2 { get; set; }
    }
}
