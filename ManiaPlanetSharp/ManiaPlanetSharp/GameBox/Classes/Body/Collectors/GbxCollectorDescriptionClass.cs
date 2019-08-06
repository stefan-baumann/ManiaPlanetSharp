using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCollectorDescriptionClass
    : GbxBodyClass
    {
        public GbxCollectorDescriptionClass() { }

        [GbxAutoStringProperty(0, false)]
        public string Description { get; set; }
    }
}
