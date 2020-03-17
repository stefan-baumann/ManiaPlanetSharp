using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E007000)]
    public class VisualModelChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node Part1 { get; set; }

        [Property(SpecialPropertyType.NodeReference)]
        public Node Part2 { get; set; }
    }

    //[Chunk(0x2E007001)]
    public class VisualModel1Chunk
        : Chunk
    {
        [Property]
        public int Version { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 1)]
        public int Unknown1 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 1)]
        public int Unknown2 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 1)]
        public float Unknown3 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 9)]
        public string Path1 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 9), Condition(nameof(Path1), ConditionOperator.Equal, null)]
        public Node Unknown4 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(Version), ConditionOperator.LessThanOrEqual, 7)]
        public Node Unknown5 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(Version), ConditionOperator.LessThan, 18)]
        public Node Unknown6 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2), Condition(nameof(Version), ConditionOperator.LessThan, 9)]
        public string Path2 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2), Condition(nameof(Version), ConditionOperator.LessThan, 5)]
        public int Unknown7Count { get; set; }

        [Property(), Array(nameof(Unknown7Count)), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2), Condition(nameof(Version), ConditionOperator.LessThan, 5)]
        public VisualModelStruct1[] Unknown7 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 5)]
        public int Unknown8Count { get; set; }

        [Property(SpecialPropertyType.NodeReference), Array(nameof(Unknown8Count)), Condition(nameof(Version), ConditionOperator.Equal, 5)]
        public Node[] Unknown8 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThan, 5)]
        public int Unknown9 { get; set; }
        
        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public int Unknown10Count { get; set; }

        [Property(), Array(nameof(Unknown10Count)), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public VisualModelStruct1[] Unknown10 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2), Condition(nameof(Version), ConditionOperator.LessThanOrEqual, 16)]
        public string Unknown11 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2), Condition(nameof(Version), ConditionOperator.LessThanOrEqual, 16)]
        public string Unknown12 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2), Condition(nameof(Version), ConditionOperator.LessThanOrEqual, 16)]
        public string Unknown13 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 10)]
        public string Unknown14 { get; set; }

        [Property, Array(3), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 11), Condition(nameof(Unknown14), ConditionOperator.NotEqual, null)]
        public float[] Unknown15 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 12)]
        public string Unknown16 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 12), Condition(nameof(Unknown16), ConditionOperator.NotEqual, null)]
        public int Unknown17 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(Version), ConditionOperator.Equal, 8)]
        public Node Unknown18 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 13)]
        public int Unknown19Count { get; set; }

        [Property(), Array(nameof(Unknown19Count)), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 13)]
        public VisualModelStruct3[] Unknown19 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 13)]
        public int Unknown20Count { get; set; }

        [Property(), Array(nameof(Unknown20Count)), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 13)]
        public VisualModelStruct4[] Unknown20 { get; set; }
        
        [Property(), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 14)]
        public VisualModelStruct3 Unknown21 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 15)]
        public string Unknown22 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 16)]
        public Node Unknown23 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 19)]
        public float Unknown24 { get; set; }

        [Property(), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 20)]
        public VisualModelStruct3 Unknown25 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 21)]
        public FileReference Unknown26 { get; set; }

    }

    //[Chunk(0x2E007002)]
    public class VisualModel2Chunk
        : Chunk
    {
        [Property]
        public int Version { get; set; }

        [Property]
        public string Unknown1 { get; set; }

        [Property]
        public string Unknown2 { get; set; }

        [Property]
        public string Unknown3 { get; set; }

        [Property]
        public string Unknown4 { get; set; }

        [Property]
        public string Unknown5 { get; set; }

        [Property, Array(12)]
        public float[] Unknown6 { get; set; }
    }

    //[Chunk(0x2E007003)]
    public class VisualModel3Chunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node Node { get; set; }
    }

    [CustomStruct]
    public class VisualModelStruct1
    {
        [Property]
        public int Version { get; set; }

        [Property]
        public int Unknown1 { get; set; }

        [Property]
        public int Unknown2 { get; set; }

        [Property]
        public float Unknown3 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public int Unknown4 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public int Unknown5 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public int Unknown6 { get; set; }
    }

    [CustomStruct]
    public class VisualModelStruct2
    {
        [Property]
        public int Unknown1 { get; set; }

        [Property, Array(7)]
        public float[] Unknown2 { get; set; }
    }

    [CustomStruct]
    public class VisualModelStruct3
    {
        [Property]
        public string Unknown1 { get; set; }

        [Property(SpecialPropertyType.NodeReference), Condition(nameof(Unknown1), ConditionOperator.Equal, null)]
        public Node Unknown2 { get; set; }
    }

    [CustomStruct]
    public class VisualModelStruct4
    {
        [Property]
        public float Unknown1 { get; set; }

        [Property]
        public float Unknown2 { get; set; }

        [Property]
        public float Unknown3 { get; set; }

        [Property]
        public bool Unknown4 { get; set; }

        [Property]
        public float Unknown5 { get; set; }
    }
}
