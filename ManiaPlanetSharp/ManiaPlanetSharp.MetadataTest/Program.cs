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

namespace ManiaPlanetSharp.MetadataTest
{
    class Program
    {
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
                            var result = new GbxFileParser(stream).Parse();

                            //Output all parsed data to the console
                            Console.WriteLine(new string('=', Console.WindowWidth));
                            Console.Write("Result: ");
                            Utils.PrintRecursive(result);
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
    }
}
