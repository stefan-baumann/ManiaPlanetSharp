using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectBaseAttributeClass
    : GbxClass
    {
        public GbxObjectBaseAttributeClass() { }

        [GbxAutoProperty(0)]
        GbxNode BaseAttributes { get; set; }
    }
}
