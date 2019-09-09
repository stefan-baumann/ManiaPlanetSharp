using AgileObjects.ReadableExpressions;
using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public static class ParserCodeGenerator
    {
        public static string GenerateChunkParserCode<TChunk>()
            where TChunk : Chunk, new()
        {
            IParser<TChunk> parser = ChunkParser<TChunk>.GenerateParser();
            Expression<ChunkParserDelegate<TChunk>> expression = (Expression<ChunkParserDelegate<TChunk>>)typeof(ChunkParser<TChunk>).GetProperty("ParserExpression", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(parser);
            return expression.Body.ToReadableString();
        }

        public static List<uint> GetParserChunkIds<TChunk>()
            where TChunk : Chunk, new()
        {
            return typeof(TChunk).GetCustomAttributes<ChunkAttribute>().Select(c => c.Id).ToList();
        }

        public static string GenerateCustomStructParserCode<TStruct>()
            where TStruct : new()
        {
            IParser<TStruct> parser = CustomStructParser<TStruct>.GenerateParser();
            Expression<ParserDelegate<TStruct>> expression = (Expression<ParserDelegate<TStruct>>)typeof(CustomStructParser<TStruct>).GetProperty("ParserExpression", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(parser);
            return expression.Body.ToReadableString();
        }
    }
}
