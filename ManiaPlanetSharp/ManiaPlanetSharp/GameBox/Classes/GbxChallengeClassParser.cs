using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class GbxChallengeClass
        : GbxNode
    { }

    public abstract class GbxChallengeClassParser<TChallengeClass>
        : IGbxChallengeClassParser<TChallengeClass>
        where TChallengeClass : GbxChallengeClass, new()
    {
        protected abstract int Chunk { get; }
        
        public /*override*/ bool CanParse(uint chunkId)
        {
            return ((chunkId >> 24) & 0xff) == 3 && ((chunkId >> 12) & 0xfff) == 0x43 && (chunkId & 0xfff) == this.Chunk;
        }

        public abstract TChallengeClass ParseChunk(GbxReader chunk);
    }

    public static class GbxChallengeClassParser
    {
        private static IGbxChallengeClassParser<GbxChallengeClass>[] Parsers = new IGbxChallengeClassParser<GbxChallengeClass>[]
        {
            new GbxTmDescriptionClassParser(),
            new GbxCommonClassParser(),
            new GbxVersionClassParser(),
            new GbxCommunityClassParser(),
            new GbxThumbnailClassParser(),
            new GbxAuthorClassParser()
        };

        public static IGbxChallengeClassParser<GbxChallengeClass> GetParser(uint chunkId)
        {
            return GbxChallengeClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
