using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorCatalogClass
        : Node
    {
        public GbxCollectorCatalogClass()
        { }

        [AutoParserProperty(0)]
        public bool IsInternal { get; set; }
        [AutoParserProperty(1)]
        public uint Unused1 { get; set; }
        [AutoParserProperty(2)]
        public uint CatalogPosition { get; set; }
        [AutoParserProperty(3)]
        public uint Unused2 { get; set; }
        [AutoParserProperty(4)]
        public uint Unused3 { get; set; }
        [AutoParserProperty(5)]
        public uint Unused4 { get; set; }
    }
}
