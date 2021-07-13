﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    /// <summary>
    /// Central factory that provides parsers for chunks and custom structs, either precompiled or dynamically generated.
    /// </summary>
    public static class ParserFactory
    {
        /// <summary>
        /// Static initialisation of the <c>ParserFactory</c> class.
        /// </summary>
        static ParserFactory()
        {
            ParserFactory.InitializePrecompiledParsers();

            ParserFactory.ScanForParseableChunks();
        }


        /// <summary>
        /// Tries to load the <c>ManiaPlanetSharp.GameBox.AutoGenerated</c> assembly containing the precompiled parsers. This method is run automatically at the first call to the <c>ParserFactory</c>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static void InitializePrecompiledParsers()
        {
            if (GlobalParserSettings.UsePrecompiledParsersIfPresent)
            {
                Debug.WriteLine("Initializing Parser Factory...");
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).Where(p => p.FullName.Contains("ManiaPlanetSharp.GameBox.AutoGenerated")).ToArray();
                var autoGeneratedParserClass = assemblies.Select(a => a.DefinedTypes.FirstOrDefault(t => t.FullName == "ManiaPlanetSharp.GameBox.Parsing.ParserGeneration.AutoGenerated.AutoGeneratedParsers")).FirstOrDefault(t => t != null);
                if (autoGeneratedParserClass == null)
                {
                    try
                    {
                        var parserAssembly = Assembly.Load("ManiaPlanetSharp.GameBox.AutoGenerated");
                        autoGeneratedParserClass = parserAssembly.DefinedTypes.FirstOrDefault(t => t.FullName == "ManiaPlanetSharp.GameBox.Parsing.ParserGeneration.AutoGenerated.AutoGeneratedParsers");
                    }
                    catch
                    { }
                    if (autoGeneratedParserClass == null)
                    {
                        Debug.WriteLine("No pre-generated parsers located.");
                    }
                }
                if (autoGeneratedParserClass != null)
                {
                    Debug.WriteLine("Located pre-generated parsers.");
                    foreach (var parser in (Dictionary<Type, IChunkParser<Chunk>>)autoGeneratedParserClass.GetProperty("ChunkParsers", BindingFlags.Public | BindingFlags.Static).GetValue(null))
                    {
                        Debug.WriteLine("    Found chunk parser for type " + parser.Key.ToString());
                        if (ParserFactory.chunkParsers.TryAdd(parser.Key, parser.Value))
                        {
                            foreach (var chunkId in parser.Value.ParseableIds)
                            {
                                chunkParsersByID.Add(chunkId.Item1, parser.Value);
                                chunkIds.Add(chunkId.Item1);
                            }
                        }
                    }
                    foreach (var parser in (Dictionary<Type, IParser<object>>)autoGeneratedParserClass.GetProperty("StructParsers", BindingFlags.Public | BindingFlags.Static).GetValue(null))
                    {
                        Debug.WriteLine("    Found struct parser for type " + parser.Key.ToString());
                        ParserFactory.structParsers.TryAdd(parser.Key, parser.Value);
                    }
                }
            }
        }

        public static void ScanForParseableChunks()
        {
            foreach (uint parseableId in AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).SelectMany(a => a.DefinedTypes).Where(t => typeof(Chunk).IsAssignableFrom(t.AsType())).SelectMany(t => t.GetCustomAttributes<ChunkAttribute>().Select(a => a.Id)))
            {
                if (!chunkIds.Contains(parseableId))
                {
                    chunkIds.Add(parseableId);
                }
            }
        }

        /// <summary>
        /// Clears the cached parsers.
        /// </summary>
        public static void ClearParsers()
        {
            chunkParsers.Clear();
            chunkParsersByID.Clear();
            chunkIds.Clear();
        }

        private static readonly ConcurrentDictionary<Type, IChunkParser<Chunk>> chunkParsers = new ConcurrentDictionary<Type, IChunkParser<Chunk>>();
        private static readonly Dictionary<uint, IChunkParser<Chunk>> chunkParsersByID = new Dictionary<uint, IChunkParser<Chunk>>();
        private static readonly HashSet<uint> chunkIds = new HashSet<uint>(GetParseableIds());

        /// <summary>
        /// Returns a parser for the specified chunk type either from the precompiled parsers or the cache of previously dynamically generated parsers or generates one dynamically.
        /// </summary>
        /// <typeparam name="TChunk">The type of chunk to be parsed</typeparam>
        /// <returns></returns>
        public static ChunkParser<TChunk> GetChunkParser<TChunk>()
            where TChunk : Chunk, new()
        {
            return (ChunkParser<TChunk>)chunkParsers.GetOrAdd(typeof(TChunk), _ =>
            {
                Debug.WriteLine($"Generating parser for type {typeof(TChunk).Name}.");
                var parser = ChunkParser<TChunk>.GenerateParser();
                foreach (var chunkId in parser.ParseableIds)
                {
                    chunkParsersByID.Add(chunkId.Item1, parser);
                    chunkIds.Add(chunkId.Item1);
                }
                return parser;
            });
        }

        /// <summary>
        /// Returns a parser for the specified chunk type either from the precompiled parsers or the cache of previously dynamically generated parsers or generates one dynamically.
        /// </summary>
        /// <param name="chunkId">The chunk id to be parsed.</param>
        /// <returns></returns>
        public static IChunkParser<Chunk> GetChunkParser(uint chunkId)
        {
            if (ParserFactory.TryGetChunkParser(chunkId, out IChunkParser<Chunk> parser))
            {
                return parser;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a parser for the specified chunk type either from the precompiled parsers or the cache of previously dynamically generated parsers or generates one dynamically.
        /// </summary>
        /// <param name="chunkId">The chunk id to be parsed.</param>
        /// <param name="parser">The chunk parser.</param>
        /// <returns><c>true</c>, if a parser could be found or generated; <c>false</c> if no parser is available.</returns>
        public static bool TryGetChunkParser(uint chunkId, out IChunkParser<Chunk> parser)
        {
            if (chunkParsersByID.TryGetValue(ClassIds.MapId(chunkId), out parser))
            {
                return true;
            }
            else if (GlobalParserSettings.UseDynamicallyCompiledChunkParsers)
            {
                Type chunkType = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(p => !p.IsDynamic)
                    .SelectMany(a => a.DefinedTypes)
                    .Where(t => typeof(Chunk).IsAssignableFrom(t.AsType()))
                    .SelectMany(t => t.GetCustomAttributes<ChunkAttribute>().Select(a => Tuple.Create(t, a.Id)))
                    .FirstOrDefault(t => t.Item2 == ClassIds.MapId(chunkId))
                    .Item1?.AsType();
                if (chunkType != null)
                {
                    parser = (IChunkParser<Chunk>)typeof(ChunkParser<>).MakeGenericType(chunkType).GetMethod("GenerateParser", BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
                    return true;
                }
            }
            parser = null;
            return false;
        }

        internal static IEnumerable<uint> GetParseableIds()
        {
            return chunkParsersByID.Keys;
        }

        public static bool IsParseableChunkId(uint id)
        {
            return chunkIds.Contains(id) || chunkIds.Contains(ClassIds.MapId(id));
        }



        private static readonly ConcurrentDictionary<Type, IParser<object>> structParsers = new ConcurrentDictionary<Type, IParser<object>>();

        public static CustomStructParser<TStruct> GetCustomStructParser<TStruct>()
            where TStruct : new()
        {
            return (CustomStructParser<TStruct>)structParsers.GetOrAdd(typeof(TStruct), _ =>
            {
                Debug.WriteLine($"Generating parser for type {typeof(TStruct).Name}.");
                return (IParser<object>)CustomStructParser<TStruct>.GenerateParser();
            });
        }
    }
}
