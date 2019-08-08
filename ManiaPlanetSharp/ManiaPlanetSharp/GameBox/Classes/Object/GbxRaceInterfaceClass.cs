using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class GbxRaceInterfaceClass
        : GbxClass
    {
        public GbxRaceInterfaceClass() { }

        [GbxAutoProperty(0)]
        public GbxNode RaceInterface { get; set; }
    }
}
