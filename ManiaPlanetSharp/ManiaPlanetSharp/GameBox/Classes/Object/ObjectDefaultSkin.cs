using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectDefaultSkin
        : Node
    {
        [AutoParserProperty(0)]
        FileReference DefaultSkin { get; set; } //Might be node
    }
}
