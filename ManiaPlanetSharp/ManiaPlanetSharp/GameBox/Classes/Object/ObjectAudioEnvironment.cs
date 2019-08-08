using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectAudioEnvironment
        : Node
    {
        [AutoParserProperty(0)]
        public Node InCarAudioEnvironment { get; set; }
    }
}
