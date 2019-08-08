using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /*public abstract class GbxClass
        : GbxNode
    { }

    public abstract class GbxClassParser<THeaderClass>
        : IGbxHeaderClassParser<THeaderClass>
        where THeaderClass : GbxHeaderClass, new()
    {
        protected abstract int ChunkId { get; }
        
        public bool CanParse(uint chunkId)
        {
            return chunkId == this.ChunkId;
        }

        public abstract THeaderClass ParseChunkInternal(GbxReader reader);
    }*/

    public static class GbxHeaderClassParser
    {
        private static IGbxClassParser<GbxClass>[] Parsers = new IGbxClassParser<GbxClass>[]
        {
            //Map
            new GbxTmDescriptionClassParser(),
            new GbxCommonClassParser(),
            new GbxVersionClassParser(),
            new GbxMapCommunityClassParser(),
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

        public static IGbxClassParser<GbxClass> GetParser(uint chunkId)
        {
            return GbxHeaderClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
