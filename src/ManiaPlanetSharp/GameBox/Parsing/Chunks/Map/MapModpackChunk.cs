using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043019, Skippable = true)]
    public class MapModpackChunk
        : Chunk
    {
        [Property]
        public FileReference Modpack { get; set; }
    }
}
