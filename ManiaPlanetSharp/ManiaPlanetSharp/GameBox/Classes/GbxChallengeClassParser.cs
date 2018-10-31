using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class GbxChallengeClass
    { }

    public abstract class GbxChallengeClassParser<TChallengeClass>
        : IGbxChallengeClassParser<TChallengeClass>
        where TChallengeClass : GbxChallengeClass, new()
    {
        protected abstract int Chunk { get; }

        protected abstract TChallengeClass ParseChunk(GbxReader reader);

        public /*override*/ bool CanParse(uint chunkId)
        {
            return ((chunkId >> 24) & 0xff) == 3 && ((chunkId >> 12) & 0xfff) == 0x43 && (chunkId & 0xfff) == this.Chunk;
        }

        public /*override*/ TChallengeClass ParseChunk(GbxNode chunk)
        {
            using (MemoryStream chunkStream = chunk.GetDataStream())
            using (GbxReader reader = new GbxReader(chunkStream))
            {
                try
                {
                    return this.ParseChunk(reader);
                }
                catch
                {
                    return new TChallengeClass();
                }
            }
        }
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
