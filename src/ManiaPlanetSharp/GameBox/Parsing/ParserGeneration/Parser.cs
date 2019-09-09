using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    public interface IParser<out T>
    { }

    public abstract class Parser<T>
        : IParser<T>
        where T : new()
    {
        protected Parser()
        { }

        protected Parser(Expression<Func<GameBoxReader, T>> expression)
            : this()
        {
            this.ParserExpression = expression;
            this.CompiledParser = expression.Compile();
        }

        protected Expression<Func<GameBoxReader, T>> ParserExpression { get; private set; }
        protected Func<GameBoxReader, T> CompiledParser { get; set; }

        public virtual T Parse(GameBoxReader reader)
        {
            return this.CompiledParser(reader);
        }
    }

    public class ChunkParser<TChunk>
        : Parser<TChunk>
        where TChunk : Chunk, new()
    {
        internal ChunkParser()
            : base()
        { }

        protected ChunkParser(Expression<Func<GameBoxReader, TChunk>> expression)
            : base(expression)
        { }

        public static ChunkParser<TChunk> GenerateParser()
        {
            var parser = new ChunkParser<TChunk>(ParserGenerator.GenerateParserExpression<TChunk>());
            parser.ParseableIds.AddRange(typeof(TChunk).GetCustomAttributes<ChunkAttribute>().Select(c => c.Id));
            return parser;
        }

        public virtual List<uint> ParseableIds { get; } = new List<uint>();
    }

    public abstract class PregeneratedChunkParser<TChunk>
        : ChunkParser<TChunk>
        where TChunk : Chunk, new()
    {
        public abstract override TChunk Parse(GameBoxReader reader);

        public abstract override List<uint> ParseableIds { get; }
    }

    public class CustomStructParser<TStruct>
        : Parser<TStruct>
        where TStruct : new()
    {
        protected CustomStructParser()
            : base()
        { }

        protected CustomStructParser(Expression<Func<GameBoxReader, TStruct>> expression)
            : base(expression)
        { }

        public static CustomStructParser<TStruct> GenerateParser()
        {
            return new CustomStructParser<TStruct>(ParserGenerator.GenerateParserExpression<TStruct>());
        }
    }

    public abstract class PregeneratedCustomStructParser<TStruct>
        : CustomStructParser<TStruct>
        where TStruct : new()
    {
        public abstract override TStruct Parse(GameBoxReader reader);
    }
}
