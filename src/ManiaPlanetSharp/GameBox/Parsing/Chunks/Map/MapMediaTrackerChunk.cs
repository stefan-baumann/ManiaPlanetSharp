using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x03043021)]
    public class MapMediaTrackerChunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node IntroClip { get; set; }

        [Property(SpecialPropertyType.NodeReference)]
        public Node InGameClip { get; set; }

        [Property(SpecialPropertyType.NodeReference)]
        public Node EndRaceClip { get; set; }
    }
}
