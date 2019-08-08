using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorNameClass
        : Node
    {
        public GbxCollectorNameClass() { }

        [AutoParserStringProperty(0, false)]
        public string Name { get; set; }
    }
}
