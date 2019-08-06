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
        GbxVec3D GroundPoint { get; set; }
        [GbxAutoProperty(1)]
        float PainterGroundMargin { get; set; }
        [GbxAutoProperty(2)]
        float OrbitalCenterHeightFromGround { get; set; }
        [GbxAutoProperty(3)]
        float OrbitalRadiusBase { get; set; }
        [GbxAutoProperty(4)]
        float OrbitalPreviewAngle { get; set; }
    }
}
