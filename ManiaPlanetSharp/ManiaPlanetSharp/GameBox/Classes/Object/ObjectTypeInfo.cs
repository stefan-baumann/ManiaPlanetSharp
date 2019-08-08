using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectTypeInfo
        : Node
    {
        [AutoParserProperty(0)]
        public uint ObjectType { get; set; }
    }
}
