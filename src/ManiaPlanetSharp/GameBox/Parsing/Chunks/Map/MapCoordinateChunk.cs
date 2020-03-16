using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043025)]
    public class MapCoordinateChunk
        : Chunk
    {
        [Property]
        public Vector2D Origin { get; set; }

        [Property]
        public Vector2D Target { get; set; }
    }
}
