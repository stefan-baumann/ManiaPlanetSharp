using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectBaseAttributes
        : Node
    {
        [AutoParserProperty(0)]
        Node BaseAttributes { get; set; }
    }
}
