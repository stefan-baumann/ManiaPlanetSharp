using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.CustomParserGenerationTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes);
            var chunks = types.Where(t => typeof(Chunk).IsAssignableFrom(t) && t != typeof(Chunk) && t.DeclaredConstructors.Any(c => c.IsPublic && c.GetParameters().Length == 0));
            var structs = types.Where(t => t.GetCustomAttribute<CustomStructAttribute>() != null && t.DeclaredConstructors.Any(c => c.IsPublic && c.GetParameters().Length == 0));
            var chunkFactory = typeof(ParserFactory).GetMethod(nameof(ParserFactory.GetChunkParser), new Type[] { });
            var structFactory = typeof(ParserFactory).GetMethod(nameof(ParserFactory.GetCustomStructParser), BindingFlags.Public | BindingFlags.Static);

            ParserFactory.InitializePrecompiledParsers();
            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            Console.WriteLine("Found chunks: " + string.Join(", ", chunks.Select(c => c.Name)));
            Console.WriteLine("Found custom structs: " + string.Join(", ", structs.Select(c => c.Name)));

            while (true)
            {
                Console.WriteLine(new string('-', Console.BufferWidth - 1));
                Console.Write("Enter name of type to generate parser for: ");
                string type = Console.ReadLine();
                Console.WriteLine();
                Type matchingType = types.FirstOrDefault(c => c.Name.ToLowerInvariant() == type.ToLowerInvariant());
                MethodInfo factoryMethod = null;
                if (chunks.Contains(matchingType))
                {
                    factoryMethod = chunkFactory.MakeGenericMethod(matchingType);

                    Console.WriteLine("Code generation:");
                    Console.WriteLine(typeof(ParserCodeGenerator).GetMethod(nameof(ParserCodeGenerator.GenerateChunkParserString), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(matchingType).Invoke(null, null));
                    Console.WriteLine("\nExpression Tree:");
                    Console.WriteLine((string)typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(typeof(ParserGenerator).GetMethod(nameof(ParserGenerator.GenerateChunkParserExpression), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(matchingType).Invoke(null, null)));
                }
                else if (structs.Contains(matchingType))
                {
                    factoryMethod = structFactory.MakeGenericMethod(matchingType);

                    Console.WriteLine("Code generation:");
                    Console.WriteLine(typeof(ParserCodeGenerator).GetMethod(nameof(ParserCodeGenerator.GenerateStructParserString), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(matchingType).Invoke(null, null));
                    Console.WriteLine("\nExpression Tree:");
                    Console.WriteLine((string)typeof(Expression).GetProperty("DebugView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(typeof(ParserGenerator).GetMethod(nameof(ParserGenerator.GenerateStructParserExpression), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(matchingType).Invoke(null, null)));
                }
                else
                {
                    Console.WriteLine("No matching type found.");
                }

                if (factoryMethod != null)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    factoryMethod.Invoke(null, null);
                    stopwatch.Stop();
                    Console.WriteLine(new string('-', Console.BufferWidth - 1));
                    Console.WriteLine($"ParserFactory: Took {stopwatch.Elapsed.TotalMilliseconds:#0.000}ms to get parser.");
                }
            }
        }

        public class TestChunk
            : Chunk
        {
            [Property]
            public uint Version { get; set; }

            [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 5)]
            public string Author { get; set; }

            [Property]
            public bool HasStringList { get; set; }

            [Property, Condition(nameof(HasStringList))]
            public uint StringCount { get; set; }

            [Property(SpecialPropertyType.LookbackString), Condition(nameof(HasStringList)), Condition(nameof(StringCount), ConditionOperator.GreaterThan, 0), Array(nameof(StringCount))]
            public string[] Strings { get; set; }

            [Property, Array]
            public uint[] AutoArray { get; set; }
        }
    }
}
