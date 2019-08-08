using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCollectorDefaultSkinClass
        : GbxClass
    {
        public GbxCollectorDefaultSkinClass() { }

        [GbxAutoStringProperty(0, false)]
        public string DefaultSkinName { get; set; }
    }
}
