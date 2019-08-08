using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostRespawnCount
        : Node
    {
        [AutoParserProperty(0)]
        public uint Respawns { get; set; }
    }
}
