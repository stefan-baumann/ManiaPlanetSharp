using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var chunks = types.Where(t => typeof(Chunk).IsAssignableFrom(t) && t != typeof(Chunk));
            var structs = types.Where(t => t.GetCustomAttribute<CustomStructAttribute>() != null);
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
                if (chunks.Contains(matchingType))
                {
                    Console.WriteLine(typeof(ParserCodeGenerator).GetMethod(nameof(ParserCodeGenerator.GenerateChunkParserCode), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(matchingType).Invoke(null, null));
                }
                else if (structs.Contains(matchingType))
                {
                    Console.WriteLine(typeof(ParserCodeGenerator).GetMethod(nameof(ParserCodeGenerator.GenerateCustomStructParserCode), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(matchingType).Invoke(null, null));
                }
                else
                {
                    Console.WriteLine("No matching type found.");
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
