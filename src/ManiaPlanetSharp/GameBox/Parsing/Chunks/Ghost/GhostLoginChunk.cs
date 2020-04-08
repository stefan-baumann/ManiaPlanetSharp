using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0309200F)]
    public class GhostLoginChunk
        : Chunk
    {
        [Property]
        public string Login { get; set; }
    }
}
