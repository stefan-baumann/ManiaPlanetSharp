using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxStemMaterialClass
        : GbxClass
    {
        public GbxStemMaterialClass() { }

        [GbxAutoProperty(0)]
        GbxNode StemMaterial { get; set; }
        [GbxAutoProperty(1)]
        GbxNode StemBumpMaterial { get; set; }
    }
}
