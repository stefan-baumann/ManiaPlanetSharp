using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCollectorNameClass
        : GbxBodyClass
    {
        public GbxCollectorNameClass() { }

        [GbxAutoStringProperty(0, false)]
        public string Name { get; set; }
    }
}
