using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectUsabilityClass
        : GbxBodyClass
    {
        public GbxObjectUsabilityClass() { }

        [GbxAutoProperty(0)]
        public int Version { get; set; }
        [GbxAutoProperty(1)]
        public bool IsUsable { get; set; }
    }
}
