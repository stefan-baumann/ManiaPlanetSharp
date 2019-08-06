using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManiaPlanetSharp.GameBox
{
    //public abstract class GbxObjectClass
    //: GbxNode
    //{ }

    //public abstract class GbxObjectClassParser<TObjectClass>
    //    : IGbxObjectClassParser<TObjectClass>
    //    where TObjectClass : GbxObjectClass, new()
    //{
    //    protected abstract int Chunk { get; }

    //    public /*override*/ bool CanParse(uint chunkId)
    //    {
    //        return (chunkId & 0xfff) == this.Chunk;
    //        //return ((chunkId >> 24) & 0xff) == 3 && ((chunkId >> 12) & 0xfff) == 0x43 && (chunkId & 0xfff) == this.Chunk;
    //    }

    //    public abstract TObjectClass ParseChunk(GbxReader chunk);
    //}

    //public static class GbxObjectClassParser
    //{
    //    private static IGbxObjectClassParser<GbxObjectClass>[] Parsers = new IGbxObjectClassParser<GbxObjectClass>[]
    //    {
    //        new GbxObjectTypeParser(),
    //    };

    //    public static IGbxObjectClassParser<GbxObjectClass> GetParser(uint chunkId)
    //    {
    //        return GbxObjectClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
    //    }
    //}
}
