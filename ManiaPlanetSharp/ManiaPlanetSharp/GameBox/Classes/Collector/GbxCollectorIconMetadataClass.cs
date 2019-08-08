using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorIconMetadataClass
        : Node
    {
        public GbxCollectorIconMetadataClass() { }

        [AutoParserProperty(0)]
        public bool UseAutoRenderedIcon { get; set; }
        [AutoParserProperty(1)]
        public uint QuarterRotationY { get; set; }
    }
}
