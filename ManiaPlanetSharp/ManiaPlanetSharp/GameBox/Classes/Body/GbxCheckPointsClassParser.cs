using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCheckpointsClass
        : GbxBodyClass
    {
        public int CheckpointCount { get => this.Checkpoints?.Length ?? 0; }
        public Checkpoint[] Checkpoints { get; set; }
    }

    public class Checkpoint
    {
        public uint Value1 { get; set; }
        public uint Value2 { get; set; }
        public uint Value3 { get; set; }
    }

    public class GbxCheckpointsClassParser
        : GbxBodyClassParser<GbxCheckpointsClass>
    {
        protected override int Chunk => 0x03043017;

        public override bool Skippable => true;

        protected override GbxCheckpointsClass ParseChunkInternal(GbxReader reader)
        {
            GbxCheckpointsClass checkpoints = new GbxCheckpointsClass()
            {
                Checkpoints = new Checkpoint[reader.ReadUInt32()]
            };

            for (int i = 0; i < checkpoints.CheckpointCount; i++)
            {
                checkpoints.Checkpoints[i] = new Checkpoint() { Value1 = reader.ReadUInt32(), Value2 = reader.ReadUInt32(), Value3 = reader.ReadUInt32() };
            }

            return checkpoints;
        }
    }
}
