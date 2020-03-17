using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectUsability
        : Node
    {
        [AutoParserProperty(0)]
        public int Version { get; set; }
        [AutoParserProperty(1)]
        public bool IsUsable { get; set; }
    }
}
