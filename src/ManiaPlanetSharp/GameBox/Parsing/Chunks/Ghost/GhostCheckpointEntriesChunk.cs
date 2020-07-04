using System;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [CustomStruct]
    public class CheckpointEntry
    {
        [Property]
        public uint TimeU { get; set; }

        public TimeSpan Time { get => TimeSpan.FromMilliseconds(TimeU); }

        [Property]
        public uint Flags { get; set; }
    }

    [Chunk(0x0309200B, Skippable = true)]
    public class GhostCheckpointEntriesChunk
        : Chunk
    {
        [Property, Array]
        public CheckpointEntry[] CheckpointEntries { get; set; }
    }
}
