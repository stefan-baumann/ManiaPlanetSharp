using ManiaPlanetSharp.GameBox.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.MetadataTest
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        try
                        {
                            MapMetadata map = new MapMetadataParser(stream).Parse();
                            Console.WriteLine($@"{map.Name} by {map.AuthorName} ({map.AuthorLogin})
Uid: {map.Uid}
Built in {map.Environment} ({map.Title})
Times: {map.AuthorTime}; {map.GoldTime}; {map.SilverTime}; {map.BronzeTime}
Display Cost: {map.DisplayCost}
Laps: {map.LapCount}
");
                        }
                        catch
                        {
                            Console.WriteLine("Invalid file");
                        }
                    }
                }
            }
        }
    }
}
