using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostRaceTime
        : Node
    {
        [AutoParserProperty(0)]
        public uint RaceTimeU { get; set; }
        public TimeSpan RaceTime { get => TimeSpan.FromMilliseconds(this.RaceTimeU); }
    }
}
