using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
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

            ParserFactory.InitializePrecompiledParsers();
            while (true)
            {
                Console.Write("GameBox file: ");
                string path = Console.ReadLine();
                if (path.StartsWith("\"") && path.EndsWith("\""))
                {
                    path = path[1..^1];
                }
                if (File.Exists(path))
                {
                    using MemoryStream stream = new MemoryStream(File.ReadAllBytes(path));
                    try
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        GameBoxFile file = GameBoxFile.Parse(stream);
                        double headerTime = stopwatch.Elapsed.TotalMilliseconds;
                        var chunks = file.ParseBody();
                        stopwatch.Stop();

                        //Create metadata provider and parse body with disabled console output
                        //var console = Console.Out;
                        //Console.SetOut(TextWriter.Null);
                        var metadataProvider = new MapMetadataProvider(file);
                        metadataProvider.ParseBody();
                        //Console.SetOut(console);

                        Console.WriteLine("Metadata:");
                        foreach (var property in metadataProvider.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(property => property.Name != "File"))
                        {
                            Console.WriteLine($" - {property.Name}: {property.GetValue(metadataProvider)}");
                        }

                        Console.WriteLine($"Done in {stopwatch.Elapsed.TotalMilliseconds:#0.0}ms (header: {headerTime:#0.0}ms, body: {stopwatch.Elapsed.TotalMilliseconds - headerTime:#0.0}ms).");
                    }
                    catch (ParseException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.InnerException.ToString());
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
