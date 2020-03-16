using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ManiaPlanetSharp.ParserTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));

            while (true)
            {
                Console.Write("GameBox file: ");
                string path = Console.ReadLine();
                if (path.StartsWith("\"") && path.EndsWith("\""))
                {
                    path = path.Substring(1, path.Length - 2);
                }
                if (File.Exists(path))
                {
                    using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(path)))
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        GameBoxFile file = GameBoxFile.Parse(stream);
                        stopwatch.Stop();

                        MetadataProvider metadataProvider = new ItemMetadataProvider(file);
                        Console.WriteLine("Metadata:");
                        foreach(var property in metadataProvider.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(property => property.Name != "File"))
                        {
                            Console.WriteLine($" - {property.Name}: {property.GetValue(metadataProvider)}");
                        }

                        Console.WriteLine($"Done in {stopwatch.Elapsed.TotalMilliseconds:#0.0}ms.");
                    }
                }
                else
                {
                    Console.WriteLine("Could not find the specified file.");
                }
                Console.WriteLine(new string('-', Console.WindowWidth / 2));
            }
        }
    }
}
