using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorBasicMetadataClass
        : Node
    {
        public GbxCollectorBasicMetadataClass() { }

        [AutoParserStringProperty(0, true)]
        public string Name { get; set; }
        [AutoParserStringProperty(1, true)]
        public string Collection { get; set; }
        [AutoParserStringProperty(2, true)]
        public string Author { get; set; }
    }
}
