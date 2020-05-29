using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    /// <summary>
    /// Base interface for parsers. For most cases, you should inherit from <c>ChunkParser&lt;TChunk&rt;</c> or <c>CustomStructParser&lt;TStruct&rt;</c> instead.
    /// </summary>
    /// <typeparam name="T">The type that is parsed by this parser.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "<Pending>")]
    public interface IParser<out T>
    { }

    /// <summary>
    /// Delegate for chunk parser implementations.
    /// </summary>
    /// <typeparam name="TChunk">The type of chunk to be parsed.</typeparam>
    /// <param name="reader">The reader to be parsed from.</param>
    /// <param name="chunkId">The id of the chunk to be parsed.</param>
    /// <returns>An instance of the parsed chunk.</returns>
    public delegate TChunk ChunkParserDelegate<TChunk>(GameBoxReader reader, uint chunkId) where TChunk : Chunk;

    /// <summary>
    /// Delegate for struct parser implementations.
    /// </summary>
    /// <typeparam name="T">The type of struct to be parsed.</typeparam>
    /// <param name="reader">The reader to be parsed from.</param>
    /// <returns>An instance of the parsed struct.</returns>
    public delegate T ParserDelegate<T>(GameBoxReader reader);
    
    /// <summary>
    /// Base interface for chunk parsers. For most cases, you should inherit from <c>ChunkParser&lt;TChunk&rt;</c> instead.
    /// </summary>
    /// <typeparam name="TChunk">The chunk type that is parsed by this parser.</typeparam>
    public interface IChunkParser<out TChunk>
        : IParser<TChunk>
        where TChunk : Chunk
    {
        /// <summary>
        /// The chunk ids parseable by this parser and a bool specifying whether they are skippable
        /// </summary>
        List<Tuple<uint, bool>> ParseableIds { get; }

        /// <summary>
        /// Parses the chunk with the specified chunk id from the specified reader.
        /// </summary>
        /// <param name="reader">The reader to be parsed from.</param>
        /// <param name="chunkId">The id of the chunk to be parsed.</param>
        /// <returns>An instance of the parsed chunk.</returns>
        TChunk Parse(GameBoxReader reader, uint chunkId);
    }

    /// <summary>
    /// Base class for chunk parsers.
    /// </summary>
    /// <typeparam name="TChunk">The chunk type that is parsed by this parser.</typeparam>
    public class ChunkParser<TChunk>
        : IChunkParser<TChunk>
        where TChunk : Chunk, new()
    {
        /// <summary>
        /// Creates a new empty instance for internal purposes.
        /// </summary>
        internal ChunkParser()
        { }

        /// <summary>
        /// Creates a chunk parser instance from a linq expression.
        /// </summary>
        /// <param name="expression">The uncompiled parse expression.</param>
        protected ChunkParser(Expression<ChunkParserDelegate<TChunk>> expression)
            : this()
        {
            this.ParserExpression = expression;
            this.CompiledParser = expression?.Compile();
        }


        /// <summary>
        /// Generates a parser for the specified chunk type dynamically, compiles and returns it. If you don't want to compile a new parser every time or use the precompiled parsers, use the <c>ParserFactory</c> class instead.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Pending>")]
        public static ChunkParser<TChunk> GenerateParser()
        {
            var parser = new ChunkParser<TChunk>(ParserGenerator.GenerateChunkParserExpression<TChunk>());
            parser.ParseableIds.AddRange(typeof(TChunk).GetCustomAttributes<ChunkAttribute>().Select(c => Tuple.Create(c.Id, c.Skippable)));
            return parser;
        }



        /// <summary>
        /// The chunk ids parseable by this parser.
        /// </summary>
        public virtual List<Tuple<uint, bool>> ParseableIds { get; } = new List<Tuple<uint, bool>>();

        /// <summary>
        /// The uncompiled linq expression for parsing the chunk.
        /// </summary>
        protected virtual Expression<ChunkParserDelegate<TChunk>> ParserExpression { get; private set; }

        /// <summary>
        /// The compiled linq expression for parsing the chunk.
        /// </summary>
        protected virtual ChunkParserDelegate<TChunk> CompiledParser { get; set; }



        /// <summary>
        /// Parses the chunk with the specified chunk id from the specified reader.
        /// </summary>
        /// <param name="reader">The reader to be parsed from.</param>
        /// <param name="chunkId">The id of the chunk to be parsed.</param>
        /// <returns>An instance of the parsed chunk.</returns>
        public virtual TChunk Parse(GameBoxReader reader, uint chunkId)
        {
            return this.CompiledParser(reader, chunkId);
        }
    }
    
    /// <summary>
    /// Base class for pre-generated/pre-compiled chunk parsers.
    /// </summary>
    /// <typeparam name="TChunk"></typeparam>
    public abstract class PregeneratedChunkParser<TChunk>
        : ChunkParser<TChunk>
        where TChunk : Chunk, new()
    {
        /// <summary>
        /// Parses the chunk with the specified chunk id from the specified reader.
        /// </summary>
        /// <param name="reader">The reader to be parsed from.</param>
        /// <param name="chunkId">The id of the chunk to be parsed.</param>
        /// <returns>An instance of the parsed chunk.</returns>
        public abstract override TChunk Parse(GameBoxReader reader, uint chunkId);

        /// <summary>
        /// The chunk ids parseable by this parser.
        /// </summary>
        public abstract override List<Tuple<uint, bool>> ParseableIds { get; }
    }

    /// <summary>
    /// Base class for struct parsers.
    /// </summary>
    /// <typeparam name="TStruct">The struct type that is parsed by this parser.</typeparam>
    public class CustomStructParser<TStruct>
        : IParser<TStruct>
        where TStruct : new()
    {
        /// <summary>
        /// Creates a new empty instance for internal purposes.
        /// </summary>
        internal CustomStructParser()
        { }

        /// <summary>
        /// Creates a struct parser instance from a linq expression.
        /// </summary>
        /// <param name="expression">The uncompiled parse expression.</param>
        protected CustomStructParser(Expression<ParserDelegate<TStruct>> expression)
            : this()
        {
            this.ParserExpression = expression;
            this.CompiledParser = expression?.Compile();
        }


        /// <summary>
        /// Generates a parser for the specified struct type dynamically, compiles and returns it. If you don't want to compile a new parser every time or use the precompiled parsers, use the <c>ParserFactory</c> class instead.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Pending>")]
        public static CustomStructParser<TStruct> GenerateParser()
        {
            return new CustomStructParser<TStruct>(ParserGenerator.GenerateStructParserExpression<TStruct>());
        }



        /// <summary>
        /// The uncompiled linq expression for parsing the struct.
        /// </summary>
        protected Expression<ParserDelegate<TStruct>> ParserExpression { get; private set; }

        /// <summary>
        /// The compiled linq expression for parsing the struct.
        /// </summary>
        protected ParserDelegate<TStruct> CompiledParser { get; set; }



        /// <summary>
        /// Parses the struct from the specified reader.
        /// </summary>
        /// <param name="reader">The reader to be parsed from.</param>
        /// <returns>An instance of the parsed struct.</returns>
        public virtual TStruct Parse(GameBoxReader reader)
        {
            return this.CompiledParser(reader);
        }
    }


    /// <summary>
    /// Base class for pre-generated/pre-compiled struct parsers.
    /// </summary>
    /// <typeparam name="TStruct"></typeparam>
    public abstract class PregeneratedCustomStructParser<TStruct>
        : CustomStructParser<TStruct>
        where TStruct : new()
    {
        /// <summary>
        /// Parses the struct from the specified reader.
        /// </summary>
        /// <param name="reader">The reader to be parsed from.</param>
        /// <returns>An instance of the parsed struct.</returns>
        public abstract override TStruct Parse(GameBoxReader reader);
    }
}
