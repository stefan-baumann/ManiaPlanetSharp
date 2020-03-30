using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x3043003)]
    public class MapCommonChunk
        : Chunk
    {
        [Property]
        public byte Version { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Uid { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Environment { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Author { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        [Obsolete("Raw Value, use GbxCommonClass.Kind instead", false)]
        public byte KindB { get; set; }

        public MapKind Kind { get => (MapKind)this.KindB; }
        
        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public bool Locked { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        [Obsolete("Legacy value, use GbxPasswordClass.Password instead", false)]
        public string Password { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string DecorationTimeOfDay { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string DecorationEnvironment { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string DecorationEnvironmentAuthor { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 3)]
        public Vector2D Origin { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 4)]
        public Vector2D Target { get; set; }

        [Property, Array(16), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 5)]
        public byte[] Unused { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6)]
        public string Type { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6)]
        public string Style { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6), Condition(nameof(Version), ConditionOperator.LessThanOrEqual, 8)]
        public bool UnusedFlag { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 8)]
        public ulong LightmapCacheUid { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 9)]
        public byte LightmapVersion { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 11)]
        public string TitleUid { get; set; }
    }
}
