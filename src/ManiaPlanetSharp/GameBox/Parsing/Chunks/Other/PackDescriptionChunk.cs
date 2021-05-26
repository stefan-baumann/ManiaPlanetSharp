using System;
using System.Collections.Generic;
using System.Text;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03059000), Chunk(0x03059001), Chunk(0x03059002)]
    public class PackDescriptionChunk
        : Chunk
    {
        [Property]
        public string Text { get; set; }

        [Property, Condition(nameof(PackDescriptionChunk.ChunkId), ConditionOperator.Equal, 0)]
        public string Ignored { get; set; }

        [Property, Condition(nameof(PackDescriptionChunk.ChunkId), ConditionOperator.GreaterThanOrEqual, 1)]
        public FileReference Pack { get; set; }

        [Property, Condition(nameof(PackDescriptionChunk.ChunkId), ConditionOperator.GreaterThanOrEqual, 2)]
        public FileReference ParentPack { get; set; }
    }
}
