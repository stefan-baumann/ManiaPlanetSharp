using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Classes.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.LightmapExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("This utility will extract all icons from gbx files in the specified directory and subdirectories and place them into the same folder as the original gbx.");
            while (true)
            {
                // Console.Write("Please enter a path to the origin directory: ");
                string path = Console.ReadLine();
                if (path.StartsWith("\"") && path.EndsWith("\""))
                {
                    path = path.Substring(1, path.Length - 2);
                }
                if (Directory.Exists(path))
                {
                    foreach (var file in Directory.GetFiles(path, "*.Map.Gbx", SearchOption.AllDirectories))
                    {
                        Console.Write($"{file.Replace(path, "")}: ");
                        var parsed = new GameBox.GameBoxFileParser(File.OpenRead(file)).Parse();
                        var headerNodes = parsed.Body
    .Where(node => !(new[] { typeof(Node), typeof(UnusedClass) }).Contains(node.GetType()))
    .Select(node => Tuple.Create(node.GetType(), node))
    .Distinct(new NodeTypeComparer())
    .ToDictionary(node => node.Item1, node => node.Item2);
                        var lightmapNode = headerNodes[typeof(GbxLightmapClass)];
                        string imagePath = Regex.Replace(file, "(\\.\\w+)?\\.[gG][bB][xX]", ".lightmap");
                        Console.WriteLine("Icon found and extracted.");
                    }
                }
                else
                {
                    Console.WriteLine("Directory could not be found.");
                }
            }
        }

        private class NodeTypeComparer
    : IEqualityComparer<Tuple<Type, Node>>
        {
            bool IEqualityComparer<Tuple<Type, Node>>.Equals(Tuple<Type, Node> x, Tuple<Type, Node> y)
            {
                return x?.Item1 == y?.Item1;
            }

            int IEqualityComparer<Tuple<Type, Node>>.GetHashCode(Tuple<Type, Node> obj)
            {
                return obj?.Item1.GetType().GetHashCode() ?? 0;
            }
        }
    }
}
