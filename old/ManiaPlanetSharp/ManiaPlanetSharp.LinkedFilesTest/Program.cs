using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.LinkedFilesTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Print all Debug output to console
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            while (true)
            {
                Console.Write("Please enter a path to a GameBox file: ");
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
                        var metadata = new ItemMetadataProvider(result);
                        Console.WriteLine($"Linked Files:\n - Mesh: {metadata.MeshName ?? "-"}\n - Shape: {metadata.ShapeName ?? "-"}\n - Trigger Shape: {metadata.TriggerShapeName ?? "-"}");
                        
                        Console.WriteLine(new string('-', Console.WindowWidth / 2));
                    }
                }
            }
        }
    }
}
