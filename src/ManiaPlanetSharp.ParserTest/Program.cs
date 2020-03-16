using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;
using System;
using System.Diagnostics;
using System.IO;

namespace ManiaPlanetSharp.ParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));

            while (true)
            {
                Console.Write("Please enter a path to a GameBox file: ");
                string path = Console.ReadLine();
                if (path.StartsWith("\"") && path.EndsWith("\""))
                {
                    path = path.Substring(1, path.Length - 2);
                }
                if (File.Exists(path))
                {
                    using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(path)))
                    using (GameBoxReader reader = new GameBoxReader(stream))
                    {
                        CustomStructParser<GameBoxFile> parser = ParserFactory.GetCustomStructParser<GameBoxFile>();
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        GameBoxFile file = parser.Parse(reader);
                        stopwatch.Stop();
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
