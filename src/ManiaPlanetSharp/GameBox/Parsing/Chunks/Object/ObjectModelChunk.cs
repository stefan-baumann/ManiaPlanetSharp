using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002019)]
    public class ObjectModelChunk
        : Chunk
    {
        [Property]
        public int Version { get; set; }

        //if ((itemType != Ornament && itemType != Spot && itemType != PickUp) || (version < 9)) && (itemType != Vehicle || (version < 10)) && (version < 12):
        [Property(SpecialPropertyType.NodeReference)]
        public Node PhysicalModel { get; set; }

        //Same condition as PhysicalModel
        [Property(SpecialPropertyType.NodeReference)]
        public Node VisualModel { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.Equal, 1)]
        public Node VisualModelStatic { get; set; }
        
        [Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 3)]
        public int Unknown1 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 4)]
        public Node Unknown2 { get; set; }

        [Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 5)]
        public Node Unknown3 { get; set; }

        [Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 6)]
        public int UnknownCount { get; set; }

        [Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 7)]
        public int Unknown4 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 8)]
        public Node Unknown5 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 8), Condition(nameof(ObjectModelChunk.Unknown5), ConditionOperator.Equal, null)]
        public Node Unknown6 { get; set; }
        
    }
}
