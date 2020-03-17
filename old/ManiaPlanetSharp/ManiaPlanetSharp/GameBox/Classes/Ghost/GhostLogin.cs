using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostLogin
        : Node
    {
        [AutoParserStringProperty(0, false)]
        public string Login { get; set; }
    }
}
