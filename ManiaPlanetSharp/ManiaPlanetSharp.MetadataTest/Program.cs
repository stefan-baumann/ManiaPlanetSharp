using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManiaPlanetSharp.Utilities;
using System.Diagnostics;
using ManiaPlanetSharp.GameBox;
using System.Reflection;
using ManiaPlanetSharp.GameBox.MetadataProviders;

namespace ManiaPlanetSharp.MetadataTest
{
    static class Program
    {
        /* TODO:
         * - [ ] Check if 0x03043018 (GbxLapCountClass) actually counts laps or checkpoints
         * 
         * 
         */

        static void Main(string[] args)
        {
            //Print all Debug output to console
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            while (true)
            {
                Console.Write("Please enter a path to a GameBox map file: ");
                string path = Console.ReadLine();
                if (path.StartsWith("\""))
                {
                    path = path.Substring(1, path.Length - 2);
                }
                if (File.Exists(path))
                {
                    using (var stream = File.OpenRead(path))
                    {
#if !DEBUG
                        try
                        {
#endif
                            var result = new GameBoxFileParser(stream).Parse();

                            //Output all parsed data to the console
                            Console.WriteLine(new string('-', Console.WindowWidth / 2));

                            //Console.WriteLine(Utils.PrintNodeTree(result));
                            //var metadata = new MapMetadataProvider(result);
                            foreach(Node node in result.Body.Where(node => node.GetType() == typeof(Node)).Where(node => node.Class != 0))
                            {
                                PrintUnknownNode(node);
                            }
                            Console.WriteLine(new string('-', Console.WindowWidth / 2));
#if !DEBUG
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid file");
                            Console.WriteLine($"{ex.GetType().Name}\n{ex.Message} {(ex.InnerException != null ? $"\n\nInner Exception: {ex.InnerException.GetType().Name}\n{ex.InnerException.Message}" : "")}");
                        }
#endif
                    }
                }
            }
        }

        static void PrintUnknownNode(Node node)
        {
            Console.WriteLine($"### {KnownClassIds.GetClassName(node.Class & ~0xFFFU) ?? "Unknown"} (0x{node.Class:X8})");
            Console.WriteLine($"Length: {node.Data.Length}");
            Console.WriteLine("```");
            if (node.Data.Length <= 256)
            {
                foreach (var batch in node.Data.Batch(16))
                {
                    StringBuilder binaryBuilder = new StringBuilder();
                    StringBuilder characterBuilder = new StringBuilder();
                    int i = 0;
                    foreach (byte b in batch)
                    {
                        if (i % 2 == 0) binaryBuilder.Append(' ');
                        binaryBuilder.Append(b.ToString("X8").Last());
                        char character = (char)b;
                        characterBuilder.Append(char.IsControl(character) ? '.' : character);

                        i++;
                    }
                    Console.WriteLine($"   {binaryBuilder.ToString(),-24}    {characterBuilder.ToString()}");
                }
            }
            Console.WriteLine("```");
        }

        //Source: http://blogs.msdn.com/b/pfxteam/archive/2012/11/16/plinq-and-int32-maxvalue.aspx
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using (var enumerator = source.GetEnumerator())
                while (enumerator.MoveNext())
                    yield return YieldBatchElements(enumerator, batchSize - 1);
        }

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize && source.MoveNext(); i++)
                yield return source.Current;
        }
    }
}
