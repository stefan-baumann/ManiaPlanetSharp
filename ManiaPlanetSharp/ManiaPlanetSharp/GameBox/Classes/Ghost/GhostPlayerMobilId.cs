using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostPlayerMobilId
        : Node
    {
        [AutoParserStringProperty(0, true)]
        public string Uid { get; set; }
    }
}
