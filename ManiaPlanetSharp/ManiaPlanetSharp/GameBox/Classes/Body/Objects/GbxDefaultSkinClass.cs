using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxDefaultSkinClass
    : GbxClass
    {
        public GbxDefaultSkinClass() { }

        [GbxAutoProperty(0)]
        GbxFileReference DefaultSkin { get; set; } //Might be node
    }
}
