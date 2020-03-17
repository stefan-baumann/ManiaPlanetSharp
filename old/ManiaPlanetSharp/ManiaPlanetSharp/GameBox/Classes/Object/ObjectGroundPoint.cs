using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectGroundPoint
        : Node
    {
        [AutoParserProperty(0)]
        public Vector3D GroundPoint { get; set; }
        [AutoParserProperty(1)]
        public float PainterGroundMargin { get; set; }
        [AutoParserProperty(2)]
        public float OrbitalCenterHeightFromGround { get; set; }
        [AutoParserProperty(3)]
        public float OrbitalRadiusBase { get; set; }
        [AutoParserProperty(4)]
        public float OrbitalPreviewAngle { get; set; }
    }
}
