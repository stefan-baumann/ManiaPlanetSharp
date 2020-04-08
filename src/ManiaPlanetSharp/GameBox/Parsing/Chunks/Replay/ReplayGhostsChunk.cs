using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093014)]
    public class ReplayGhostsChunk
        : Chunk
    {
        [Property]
        public uint Ignored1 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Array]
        public Node[] Ghosts { get; set; }

        [Property]
        public uint Ignored2 { get; set; }

        [Property, Array]
        public ulong[] Extras { get; set; }
    }
}
