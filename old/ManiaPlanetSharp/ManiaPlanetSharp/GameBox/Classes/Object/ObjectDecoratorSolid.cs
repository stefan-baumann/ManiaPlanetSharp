using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectDecoratorSolid
        : Node
    {
        [AutoParserProperty(0)]
        public Node DecoratorSolid { get; set; }
    }
}
