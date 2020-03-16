using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043054, Skippable = true)]
    public class MapEmbeddedItemsChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property]
        public uint Unknown { get; set; }

        [Property]
        public uint ZipSize { get; set; }

        public uint ActualZipSize { get; set; }

        [Property]
        public uint ItemCount { get; set; }

        [Property, CustomParserMethod(nameof(ParseItems))]
        public EmbeddedItem[] Items { get; set; }

        public EmbeddedItem[] ParseItems(GameBoxReader reader)
        {
            var start = reader.Stream.Position;
            var parser = ParserFactory.GetCustomStructParser<EmbeddedItem>();
            var items = new EmbeddedItem[this.ItemCount];
            for (int i = 0; i < this.ItemCount; i++)
            {
                this.Items[i] = parser.Parse(reader);
            }
            this.ActualZipSize = this.ZipSize - (uint)(reader.Stream.Position - start);
            return items;
        }

        [Property, Array(nameof(ActualZipSize))]
        public byte[] ZipFile { get; set; }
    }

    [CustomStruct]
    public class EmbeddedItem
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Path { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Collection { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Author { get; set; }
    }
}
