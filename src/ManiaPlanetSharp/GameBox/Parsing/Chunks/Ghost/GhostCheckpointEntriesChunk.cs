using System;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [CustomStruct]
    public class CheckpointEntry
    {
        [Property]
        public uint Time { get; set; }

        [Property]
        public uint Flags { get; set; }
    }

    [Chunk(0x0309200B, Skippable = true)]
    public class GhostCheckpointEntriesChunk
        : Chunk
    {
        [Property]
        public uint CheckpointEntryCount { get; set; }

        [Property, Array(nameof(CheckpointEntryCount))]
        public CheckpointEntry[] CheckpointEntries { get; set; }
    }
}
