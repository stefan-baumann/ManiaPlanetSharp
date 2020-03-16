using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E001003)]
    public class CollectorMetadataChunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Name { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Collection { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Author { get; set; }

        [Property]
        public uint Version { get; set; }

        [Property]
        public string Path { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.Equal, 5)]
        public string Unused1 { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 4)]
        public string Unused2 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 3)]
        public uint Unused3 { get; set; }

        //public uint IsInternal { get; set; }
        //public uint IsAdvanced { get; set; }
        //public uint IconDescription { get; set; }
        //public uint Unused4 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 4)]
        public ushort CatalogPosition { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 7)]
        public string Name2 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 8)]
        public byte ProductStateB { get; set; }
        public ProductState ProductState => (ProductState)this.ProductStateB;
    }

    public enum ProductState
        : byte
    {
        Aborted = 0,
        GameBox = 1,
        DevelopmentBuild = 2,
        Release = 3
    }
}
