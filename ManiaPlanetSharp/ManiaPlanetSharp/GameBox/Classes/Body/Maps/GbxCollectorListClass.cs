using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCollectorListClass
        : GbxClass
    {
        public int ArchiveCount { get => this.Archive?.Length ?? 0; }
        public CollectorStock[] Archive { get; set; }
    }

    public class CollectorStock
    {
        public string BlockName { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
        public uint Data { get; set; }
    }

    public class GbxCollectorListClassParser
        : GbxClassParser<GbxCollectorListClass>
    {
        protected override int ChunkId => 0x0301B000;

        protected override GbxCollectorListClass ParseChunkInternal(GbxReader reader)
        {
            GbxCollectorListClass collector = new GbxCollectorListClass() { Archive = new CollectorStock[reader.ReadUInt32()] };
            for (int i = 0; i < collector.ArchiveCount; i++)
            {
                collector.Archive[i] = new CollectorStock()
                {
                    BlockName = reader.ReadLookbackString(),
                    Collection = reader.ReadLookbackString(),
                    Author = reader.ReadLookbackString(),
                    Data = reader.ReadUInt32()
                };
            }

            return collector;
        }
    }
}
