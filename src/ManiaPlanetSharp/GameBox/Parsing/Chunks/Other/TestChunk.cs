using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.Other
{
    //[Chunk(0x12345678)]
    public class TestChunk
        : Chunk
    {
        public string Text { get; set; }

        [Property, Condition(nameof(BlockSkinChunk.ChunkId), ConditionOperator.Equal, 0)]
        public string Ignored { get; set; }

        [Property, Condition(nameof(BlockSkinChunk.ChunkId), ConditionOperator.GreaterThanOrEqual, 1)]
        public FileReference Pack { get; set; }

        [Property, Condition(nameof(BlockSkinChunk.ChunkId), ConditionOperator.GreaterThanOrEqual, 2)]
        public FileReference ParentPack { get; set; }
    }
}
