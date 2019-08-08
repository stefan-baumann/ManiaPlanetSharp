using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxFreelyAnchorableClass
        : GbxClass
    {
        public GbxFreelyAnchorableClass() { }

        [GbxAutoProperty(0)]
        public int Version { get; set; }
        [GbxAutoProperty(1)]
        public bool IsFreelyAnchorable { get; set; }
    }
}
