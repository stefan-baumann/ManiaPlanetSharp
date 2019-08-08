using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
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
            new GbxUnusedClassParser(0x2E002001, reader => reader.ReadUInt32()),

            //Collector
            new GbxCollectorMainDescriptionClassParser(),
            new GbxIconParser(),
            new GbxLightmapCacheIdParser(),
        };

        public static IGbxClassParser<GbxClass> GetParser(uint chunkId)
        {
            return GbxHeaderClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
