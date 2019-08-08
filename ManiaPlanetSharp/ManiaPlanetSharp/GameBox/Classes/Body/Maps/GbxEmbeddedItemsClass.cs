using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxEmbeddedItemsClass
        : GbxClass
    {
        public uint Version { get; set; }
        public uint Unknown { get; set; }
        public uint ChunkSize { get; set; }
        public int ItemCount { get; set; }
        public int ZipSize { get => this.ZipFile?.Length ?? 0; }
        public byte[] ZipFile { get; set; }
        public GbxEmbeddedItem[] Items { get; set; }
    }

    public class GbxEmbeddedItem
    {
        public string Path { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
    }

    public class GbxEmbeddedItemsClassParser
        : GbxClassParser<GbxEmbeddedItemsClass>
    {
        protected override int ChunkId => 0x03043054;

        public override bool Skippable => true;

        protected override GbxEmbeddedItemsClass ParseChunkInternal(GbxReader reader)
        {
            GbxEmbeddedItemsClass embeddedItems = new GbxEmbeddedItemsClass();
            embeddedItems.Version = reader.ReadUInt32();
            embeddedItems.Unknown = reader.ReadUInt32();
            uint size = reader.ReadUInt32();
            embeddedItems.ItemCount = (int)reader.ReadUInt32();
            embeddedItems.ZipFile = reader.ReadRaw((int)size);

            embeddedItems.Items = this.ParseItems(embeddedItems.ZipFile, (int)embeddedItems.ItemCount);

            return embeddedItems;
        }

        protected GbxEmbeddedItem[] ParseItems(byte[] zipFile, int itemCount)
        {
            using (MemoryStream stream = new MemoryStream(zipFile))
            using (GbxReader reader = new GbxReader(stream))
            {
                GbxEmbeddedItem[] items = new GbxEmbeddedItem[itemCount];
                for (int i = 0; i < itemCount; i++)
                {
                    GbxEmbeddedItem item = new GbxEmbeddedItem();

                    item.Path = reader.ReadLookbackString();
                    item.Collection = reader.ReadLookbackString();
                    item.Author = reader.ReadLookbackString();
                    
                    items[i] = item;
                }

                Debug.WriteLine($"    Done with parsing embedded item metadata, {stream.Position} of {stream.Length} bytes read.");

                return items;
            }
        }
    }
}
