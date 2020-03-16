using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    public interface IParser<out T>
    { }

    public delegate TChunk ChunkParserDelegate<TChunk>(GameBoxReader reader, uint chunkId);
    public delegate T ParserDelegate<T>(GameBoxReader reader);
    
    public interface IChunkParser<out TChunk>
        : IParser<TChunk>
        where TChunk : Chunk
    {
        List<uint> ParseableIds { get; }

        TChunk Parse(GameBoxReader reader, uint chunkId);
    }

    public class ChunkParser<TChunk>
        : IChunkParser<TChunk>
        where TChunk : Chunk, new()
    {
        internal ChunkParser()
        { }

        protected ChunkParser(Expression<ChunkParserDelegate<TChunk>> expression)
            : this()
        {
            this.ParserExpression = expression;
            this.CompiledParser = expression.Compile();
        }

        public static ChunkParser<TChunk> GenerateParser()
        {
            var parser = new ChunkParser<TChunk>(ParserGenerator.GenerateChunkParserExpression<TChunk>());
            parser.ParseableIds.AddRange(typeof(TChunk).GetCustomAttributes<ChunkAttribute>().Select(c => c.Id));
            return parser;
        }



        public virtual List<uint> ParseableIds { get; } = new List<uint>();

        protected Expression<ChunkParserDelegate<TChunk>> ParserExpression { get; private set; }
        protected ChunkParserDelegate<TChunk> CompiledParser { get; set; }



        public virtual TChunk Parse(GameBoxReader reader, uint chunkId)
        {
            return this.CompiledParser(reader, chunkId);
        }
    }

    public abstract class PregeneratedChunkParser<TChunk>
        : ChunkParser<TChunk>
        where TChunk : Chunk, new()
    {
        public abstract override TChunk Parse(GameBoxReader reader, uint chunkId);

        public abstract override List<uint> ParseableIds { get; }
    }

    public class CustomStructParser<TStruct>
        : IParser<TStruct>
        where TStruct : new()
    {
        internal CustomStructParser()
        { }

        protected CustomStructParser(Expression<ParserDelegate<TStruct>> expression)
            : this()
        {
            this.ParserExpression = expression;
            this.CompiledParser = expression.Compile();
        }

        public static CustomStructParser<TStruct> GenerateParser()
        {
            return new CustomStructParser<TStruct>(ParserGenerator.GenerateStructParserExpression<TStruct>());
        }



        protected Expression<ParserDelegate<TStruct>> ParserExpression { get; private set; }
        protected ParserDelegate<TStruct> CompiledParser { get; set; }



        public virtual TStruct Parse(GameBoxReader reader)
        {
            return this.CompiledParser(reader);
        }
    }

    public abstract class PregeneratedCustomStructParser<TStruct>
        : CustomStructParser<TStruct>
        where TStruct : new()
    {
        public abstract override TStruct Parse(GameBoxReader reader);
    }
}
