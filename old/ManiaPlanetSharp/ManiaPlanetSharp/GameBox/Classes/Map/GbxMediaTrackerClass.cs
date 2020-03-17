using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxMediaTrackerClass
        : Node
    {
        public Node IntroClip { get; set; }
        public Node InGameClip { get; set; }
        public Node EndRaceClip { get; set; }
    }

    public class GbxMediaTrackerClassParser
        : ClassParser<GbxMediaTrackerClass>
    {
        protected override int ChunkId => 0x03043021;

        protected override GbxMediaTrackerClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxMediaTrackerClass()
            {
                IntroClip = reader.ReadNodeReference(),
                InGameClip = reader.ReadNodeReference(),
                EndRaceClip = reader.ReadNodeReference()
            };
        }
    }
}
