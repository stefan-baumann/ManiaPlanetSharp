using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorIconMetadataClass
        : GbxClass
    {
        public GbxCollectorIconMetadataClass() { }

        [GbxAutoProperty(0)]
        public bool UseAutoRenderedIcon { get; set; }
        [GbxAutoProperty(1)]
        public uint QuarterRotationY { get; set; }
    }
}
