using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class GbxHeaderClass
        : GbxNode
    { }

    public abstract class GbxHeaderClassParser<THeaderClass>
        : IGbxHeaderClassParser<THeaderClass>
        where THeaderClass : GbxHeaderClass, new()
    {
        protected abstract int Chunk { get; }
        
        public /*override*/ bool CanParse(uint chunkId)
        {
            return chunkId == this.Chunk;
        }

        public abstract THeaderClass ParseChunk(GbxReader chunk);
    }

    public static class GbxHeaderClassParser
    {
        private static IGbxHeaderClassParser<GbxHeaderClass>[] Parsers = new IGbxHeaderClassParser<GbxHeaderClass>[]
        {
            //Map
            new GbxTmDescriptionClassParser(),
            new GbxCommonClassParser(),
            new GbxVersionClassParser(),
            new GbxCommunityClassParser(),
            new GbxThumbnailClassParser(),
            new GbxAuthorClassParser(),

            //Object
            new GbxObjectTypeParser(),
            new GbxUnusedHeaderClassParser(0x2E002001, reader => reader.ReadUInt32()),

            //Collector
            new GbxCollectorDescriptionParser(),
            new GbxIconParser(),
            new GbxLightmapCacheIdParser(),
        };

        public static IGbxHeaderClassParser<GbxHeaderClass> GetParser(uint chunkId)
        {
            return GbxHeaderClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
