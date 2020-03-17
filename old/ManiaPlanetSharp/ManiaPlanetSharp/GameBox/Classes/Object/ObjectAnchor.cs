using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectAnchor
        : Node
    {
        [AutoParserProperty(0)]
        public int Version { get; set; }
        [AutoParserProperty(1)]
        public bool IsFreelyAnchorable { get; set; }
    }
}
