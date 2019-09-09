using System;
using System.Collections.Generic;
using System.Text;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E009000)]
    public class WaypointSpecialPropertyChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property, Condition(nameof(WaypointSpecialPropertyChunk.Version), ConditionOperator.Equal, 1)]
        public uint Spawn { get; set; }

        [Property, Condition(nameof(WaypointSpecialPropertyChunk.Version), ConditionOperator.GreaterThan, 1)]
        public string Tag { get; set; }

        [Property]
        public uint Order { get; set; }
    }
}
