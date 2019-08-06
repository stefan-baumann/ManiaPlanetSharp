using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCollectorDescription
        : GbxHeaderClass
    {
        public string Name { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
        public uint Version { get; set; }
        public string Path { get; set; }
        public string Unused1 { get; set; }
        public string Unused2 { get; set; }
        public uint Unused3 { get; set; }
        //public uint IsInternal { get; set; }
        //public uint IsAdvanced { get; set; }
        //public uint IconDescription { get; set; }
        //public uint Unused4 { get; set; }
        public ushort CatalogPosition { get; set; }
        public byte ProductState { get; set; }
    }

    public class GbxCollectorDescriptionParser
        : GbxHeaderClassParser<GbxCollectorDescription>
    {
        protected override int Chunk => 0x2E001003;

        public override GbxCollectorDescription ParseChunk(GbxReader chunk)
        {
            var result = new GbxCollectorDescription();
            result.Name = chunk.ReadLoopbackString();
            result.Collection = chunk.ReadLoopbackString();
            result.Author = chunk.ReadLoopbackString();
            result.Version = chunk.ReadUInt32();
            result.Path = chunk.ReadString();
            if (result.Version == 5)
            {
                result.Unused1 = chunk.ReadLoopbackString();
            }
            if (result.Version >= 4)
            {
                result.Unused2 = chunk.ReadLoopbackString();
            }
            if (result.Version >= 3)
            {
                result.Unused3 = chunk.ReadUInt32();
                //result.IsInternal = chunk.ReadUInt32();
                //result.IsAdvanced = chunk.ReadUInt32();
                //result.IconDescription = chunk.ReadUInt32();
                //result.Unused4 = chunk.ReadUInt32();
                result.CatalogPosition = chunk.ReadUInt16();
            }
            if (result.Version >= 7)
            {
                result.Name = chunk.ReadString();
            }
            if (result.Version >= 8)
            {
                result.ProductState = chunk.ReadByte();
            }

            return result;
        }
    }
}
