using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostLighttrail
        : Node
    {
        [AutoParserProperty(0)]
        public Vector3D Color { get; set; }
    }
}
