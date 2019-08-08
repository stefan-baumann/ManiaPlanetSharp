using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
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
