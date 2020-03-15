using System;
using System.Collections.Generic;
using System.Text;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043017, true)]
    public class CheckpointsChunk
        : Chunk
    {
        [Property, Array]
        public Checkpoint[] Checkpoints { get; set; }
    }

    [CustomStruct]
    public class Checkpoint
    {
        [Property]
        public uint Value1 { get; set; }

        [Property]
        public uint Value2 { get; set; }

        [Property]
        public uint Value3 { get; set; }
    }
}
