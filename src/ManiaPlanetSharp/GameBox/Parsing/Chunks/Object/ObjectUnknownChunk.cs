using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.Object
{
    [Chunk(0x2E002001)]
    public class ObjectUnknownChunk
        : Chunk
    {
        [Property]
        public uint Unknown { get; set; }
    }
}
