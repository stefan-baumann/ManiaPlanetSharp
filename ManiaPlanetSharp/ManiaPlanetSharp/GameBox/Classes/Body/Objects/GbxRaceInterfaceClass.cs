using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxRaceInterfaceClass
        : GbxBodyClass
    {
        public GbxRaceInterfaceClass() { }

        [GbxAutoProperty(0)]
        GbxNode RaceInterface { get; set; } //Might be NodeRef?
    }
}
