using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public class PlayerProfile
        : Node
    {
        [AutoParserStringProperty(0, false)]
        public string OnlineLogin { get; set; }
        [AutoParserStringProperty(1, false)]
        public string OnlineSupportKey { get; set; }
    }
}
