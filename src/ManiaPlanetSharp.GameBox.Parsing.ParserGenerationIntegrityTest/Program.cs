using KellermanSoftware.CompareNetObjects;
using System;
using System.IO;
using System.Linq;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGenerationIntegrityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var gbxPath = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.Gbx").FirstOrDefault();
            if (!File.Exists(gbxPath))
            {
                throw new FileNotFoundException("Could not find test gbx file.");
            }
            Console.WriteLine($"Test file: {gbxPath}");

            var compareLogic = new CompareLogic(new ComparisonConfig() { MaxDifferences = 10 });

            //Runtime
            ParserFactory.ClearParsers();
            GlobalParserSettings.UseDynamicallyCompiledChunkParsers = true;
            GlobalParserSettings.UsePrecompiledParsersIfPresent = false;
            ParserFactory.ClearParsers();
            ParserFactory.ScanForParseableChunks();

            var runtimeFile = GameBoxFile.Parse(gbxPath);
            var runtimeBody = runtimeFile.ParseBody().ToList();

            //Pregenerated
            GlobalParserSettings.UseDynamicallyCompiledChunkParsers = false;
            GlobalParserSettings.UsePrecompiledParsersIfPresent = true;
            ParserFactory.ClearParsers();
            ParserFactory.InitializePrecompiledParsers();

            var pregeneratedFile = GameBoxFile.Parse(gbxPath);
            var pregeneratedBody = runtimeFile.ParseBody().ToList();

            Console.WriteLine("\n\n# Header and general file metadata");
            var fileResult = compareLogic.Compare(pregeneratedFile, runtimeFile);
            if (fileResult.AreEqual)
            {
                Console.WriteLine("Same data. OK.");
            }
            else
            {
                Console.WriteLine(fileResult.DifferencesString);
            }

            Console.WriteLine("\n\n# Body");
            var bodyResult = compareLogic.Compare(pregeneratedBody, runtimeBody);
            if (bodyResult.AreEqual)
            {
                Console.WriteLine("Same data. OK.");
            }
            else
            {
                Console.WriteLine(bodyResult.DifferencesString);
            }


            Console.ReadKey(true);
        }
    }
}
