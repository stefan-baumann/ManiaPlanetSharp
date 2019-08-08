using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCollectorBasicMetadataClass
        : GbxClass
    {
        public GbxCollectorBasicMetadataClass() { }

        [GbxAutoStringProperty(0, true)]
        public string Name { get; set; }
        [GbxAutoStringProperty(1, true)]
        public string Collection { get; set; }
        [GbxAutoStringProperty(2, true)]
        public string Author { get; set; }
    }
}
