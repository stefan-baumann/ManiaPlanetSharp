using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectMaterial
        : Node
    {
        [AutoParserProperty(0)]
        Node StemMaterial { get; set; }
        [AutoParserProperty(1)]
        Node StemBumpMaterial { get; set; }
    }
}
