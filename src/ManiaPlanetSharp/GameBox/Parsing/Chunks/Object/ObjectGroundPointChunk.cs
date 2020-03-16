using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002012)]
    public class ObjectGroundPointChunk
        : Chunk
    {
        [Property]
        public Vector3D GroundPoint { get; set; }

        [Property]
        public float PainterGroundMargin { get; set; }

        [Property]
        public float OrbitalCenterHeightFromGround { get; set; }

        [Property]
        public float OrbitalRadiusBase { get; set; }

        [Property]
        public float OrbitalPreviewAngle { get; set; }
    }
}
