using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpCompress.Archives.Zip;

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
                items[i] = parser.Parse(reader);
            }
            this.ActualZipSize = this.ZipSize - (uint)(reader.Stream.Position - start);
            return items;
        }

        [Property, Array(nameof(ActualZipSize))]
        public byte[] ZipFile { get; set; }



        public IEnumerable<EmbeddedItemFile> GetEmbeddedItemFiles()
        {
            using (MemoryStream stream = new MemoryStream(this.ZipFile))
            {
                using (ZipArchive archive = ZipArchive.Open(stream))
                {
                    return archive.Entries
                        .Where(entry => !entry.IsDirectory)
                        .Select(entry =>
                    {
                        using (MemoryStream target = new MemoryStream((int)entry.Size))
                        using (Stream source = entry.OpenEntryStream())
                        {
                            source.CopyTo(target);
                            return new EmbeddedItemFile(entry.Key, target.ToArray());
                        }
                    });
                }
            }
        }
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

    
    public class EmbeddedItemFile
    {
        public EmbeddedItemFile(string path, byte[] data)
        {
            this.Path = path;
            this.Data = data;
        }

        public string Path { get; set; }

        public byte[] Data { get; set; }

        public GameBoxFile Parse()
        {
            using (MemoryStream stream = new MemoryStream(this.Data))
            {
                return GameBoxFile.Parse(stream);
            }
        }
    }
}
