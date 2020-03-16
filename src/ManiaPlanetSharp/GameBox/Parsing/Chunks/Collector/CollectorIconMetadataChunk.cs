using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E00100E)]
    public class CollectorIconMetadataChunk
        : Chunk
    {
        [Property]
        public bool UseAutoRenderedIcon { get; set; }

        [Property]
        public uint QuarterRotationY { get; set; }
    }
}
