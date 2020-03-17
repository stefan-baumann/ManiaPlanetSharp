using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostStuntScore
        : Node
    {
        [AutoParserProperty(0)]
        public uint Score { get; set; }
    }
}
