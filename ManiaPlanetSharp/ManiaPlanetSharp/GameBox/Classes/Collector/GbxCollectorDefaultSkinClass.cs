using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorDefaultSkinClass
        : Node
    {
        public GbxCollectorDefaultSkinClass() { }

        [AutoParserStringProperty(0, false)]
        public string DefaultSkinName { get; set; }
    }
}
