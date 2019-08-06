using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxGroundPointClass
        : GbxBodyClass
    {
        public GbxGroundPointClass() { }

        [GbxAutoProperty(0)]
        public GbxVec3D GroundPoint { get; set; }
        [GbxAutoProperty(1)]
        public float PainterGroundMargin { get; set; }
        [GbxAutoProperty(2)]
        public float OrbitalCenterHeightFromGround { get; set; }
        [GbxAutoProperty(3)]
        public float OrbitalRadiusBase { get; set; }
        [GbxAutoProperty(4)]
        public float OrbitalPreviewAngle { get; set; }
    }
}
