using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxMediaTrackerClass
        : GbxClass
    {
        public GbxNode IntroClip { get; set; }
        public GbxNode InGameClip { get; set; }
        public GbxNode EndRaceClip { get; set; }
    }

    public class GbxMediaTrackerClassParser
        : GbxClassParser<GbxMediaTrackerClass>
    {
        protected override int ChunkId => 0x03043021;

        protected override GbxMediaTrackerClass ParseChunkInternal(GbxReader reader)
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
