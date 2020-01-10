using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.BatchIconExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This utility will extract all icons from gbx files in the specified directory and subdirectories and place them into the same folder as the original gbx.");
            while (true)
            {
                Console.Write("Please enter a path to the origin directory: ");
                string path = Console.ReadLine();
                if (path.StartsWith("\"") && path.EndsWith("\""))
                {
                    path = path.Substring(1, path.Length - 2);
                }
                if (Directory.Exists(path))
                {
                    foreach (var file in Directory.GetFiles(path, "*.Gbx", SearchOption.AllDirectories))
                    {
                        Console.Write($"{file.Replace(path, "")}: ");
                        var parsed = new GameBox.GameBoxFileParser(File.OpenRead(file)).Parse();
                        var metadataProvider = new GameBox.MetadataProviders.ItemMetadataProvider(parsed);
                        if ((metadataProvider.IconData?.Length ?? 0) != 0)
                        {
                            string imagePath = Regex.Replace(file, "(\\.\\w+)?\\.[gG][bB][xX]", ".png");
                            metadataProvider.GenerateIconBitmap().Save(imagePath);
                            Console.WriteLine("Icon found and extracted.");
                        }
                        else
                        {
                            Console.WriteLine("No icon found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory could not be found.");
                }
            }
        }
    }
}
