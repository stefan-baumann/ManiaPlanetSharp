using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SharpCompress.Archives.Zip;
using SharpCompress.Readers;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxEmbeddedItemsClass
        : Node
    {
        public uint Version { get; set; }
        public uint Unknown { get; set; }
        public uint ChunkSize { get; set; }
        public int ItemCount { get; set; }
        public int ZipSize { get => this.ZipFile?.Length ?? 0; }
        public byte[] ZipFile { get; set; }
        public uint EmbeddedItemSize { get; set; }
        public GbxEmbeddedItem[] Items { get; set; }
        public GbxEmbeddedItemFile[] Files { get; set; }
    }

    public class GbxEmbeddedItem
    {
        public string Path { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
    }

    public class GbxEmbeddedItemFile
    {
        public string Path { get; set; }
        public byte[] Data { get; set; }

        public Stream GetDataStream()
        {
            return new MemoryStream(this.Data);
        }

        public GameBoxFile ParseFile()
        {
            using (Stream stream = this.GetDataStream())
            {
                return new GameBoxFileParser(stream).Parse();
            }
        }
    }

    public class GbxEmbeddedItemsClassParser
        : ClassParser<GbxEmbeddedItemsClass>
    {
        protected override int ChunkId => 0x03043054;

        public override bool Skippable => true;

        protected override GbxEmbeddedItemsClass ParseChunkInternal(GameBoxReader reader)
        {
            GbxEmbeddedItemsClass embeddedItems = new GbxEmbeddedItemsClass();
            embeddedItems.Version = reader.ReadUInt32();
            embeddedItems.Unknown = reader.ReadUInt32();
            uint size = reader.ReadUInt32();
            embeddedItems.ItemCount = (int)reader.ReadUInt32();
            embeddedItems.ZipFile = reader.ReadRaw((int)size);

            using (MemoryStream stream = new MemoryStream(embeddedItems.ZipFile))
            using (GameBoxReader zipReader = new GameBoxReader(stream))
            {
                embeddedItems.Items = new GbxEmbeddedItem[embeddedItems.ItemCount];
                for (int i = 0; i < embeddedItems.ItemCount; i++)
                {
                    GbxEmbeddedItem item = new GbxEmbeddedItem();

                    item.Path = zipReader.ReadLookbackString();
                    item.Collection = zipReader.ReadLookbackString();
                    item.Author = zipReader.ReadLookbackString();

                    embeddedItems.Items[i] = item;
                }

                //Debug.WriteLine($"    Done with parsing embedded item metadata, {stream.Position} of {stream.Length} bytes read.");

                embeddedItems.EmbeddedItemSize = zipReader.ReadUInt32();
                using (MemoryStream zipStream = new MemoryStream((int)embeddedItems.EmbeddedItemSize))
                {
                    stream.CopyTo(zipStream);
                    using (ZipArchive archive = ZipArchive.Open(zipStream))
                    {
                        embeddedItems.Files = archive.Entries.Select(entry =>
                        {
                            var file = new GbxEmbeddedItemFile() { Path = entry.Key };
                            if (!entry.IsDirectory)
                            {
                                using (MemoryStream target = new MemoryStream((int)entry.Size))
                                using (Stream source = entry.OpenEntryStream())
                                {
                                    source.CopyTo(target);
                                    file.Data = target.ToArray();
                                }
                            }
                            return file;
                        }).ToArray();
                    }
                }
            }

            return embeddedItems;
        }
    }
}
