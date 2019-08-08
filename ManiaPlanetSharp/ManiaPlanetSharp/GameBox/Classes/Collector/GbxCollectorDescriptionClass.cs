using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorDescriptionClass
    : GbxClass
    {
        public GbxCollectorDescriptionClass() { }

        [GbxAutoStringProperty(0, false)]
        public string Description { get; set; }
    }
}
