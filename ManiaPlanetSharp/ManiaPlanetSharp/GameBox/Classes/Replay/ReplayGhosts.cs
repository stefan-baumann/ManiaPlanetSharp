using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Replay
{
    public class ReplayGhosts
        : Node
    {
        [AutoParserProperty(0)]
        public uint Ignored1 { get; set; }
        [AutoParserProperty(1)]
        public uint GhostCount { get; set; }
        [AutoParserArrayProperty(2, 1)]
        public Node[] Ghosts { get; set; }
        [AutoParserProperty(3)]
        public uint Ignored2 { get; set; }
        [AutoParserProperty(4)]
        public uint ExtraCount { get; set; }
        [AutoParserArrayProperty(5, 4)]
        public ulong[] Extras { get; set; }
    }
}
