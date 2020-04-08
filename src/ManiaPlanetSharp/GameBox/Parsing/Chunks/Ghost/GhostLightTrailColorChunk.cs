using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092009, Skippable = true)]
    public class GhostLightTrailColorChunk
        : Chunk
    {
        [Property]
        public Vector3D LightTrailColor { get; set; }
    }
}
