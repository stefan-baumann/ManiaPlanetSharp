using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0309200A, Skippable = true)]
    public class GhostStuntScoreChunk
        : Chunk
    {
        [Property]
        public uint StuntScore { get; set; }
    }
}
