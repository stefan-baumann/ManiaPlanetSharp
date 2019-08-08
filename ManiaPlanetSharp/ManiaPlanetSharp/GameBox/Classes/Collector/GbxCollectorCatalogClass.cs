using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorCatalogClass
        : GbxClass
    {
        public GbxCollectorCatalogClass()
        { }

        [GbxAutoProperty(0)]
        public bool IsInternal { get; set; }
        [GbxAutoProperty(1)]
        public uint Unused1 { get; set; }
        [GbxAutoProperty(2)]
        public uint CatalogPosition { get; set; }
        [GbxAutoProperty(3)]
        public uint Unused2 { get; set; }
        [GbxAutoProperty(4)]
        public uint Unused3 { get; set; }
        [GbxAutoProperty(5)]
        public uint Unused4 { get; set; }
    }
}
