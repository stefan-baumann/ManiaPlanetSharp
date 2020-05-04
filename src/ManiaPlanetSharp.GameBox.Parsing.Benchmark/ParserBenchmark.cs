using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Benchmark
{
    public abstract class ParserBenchmark
    {
        protected string gbxPath;
        protected byte[] file;
        protected GameBoxFile parsedFile;

        [GlobalSetup]
        public virtual void Setup()
        {
            this.gbxPath = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.Gbx").FirstOrDefault();
            if (!File.Exists(this.gbxPath))
            {
                throw new FileNotFoundException("Could not find test gbx file.");
            }
            Console.WriteLine($"Test file: {gbxPath}");
            this.file = File.ReadAllBytes(this.gbxPath);
            Console.WriteLine($"File length: {this.file.Length} bytes");
            this.parsedFile = GameBoxFile.Parse(new MemoryStream(this.file));
            Console.WriteLine($"Uncompressed body length: {this.parsedFile.GetUncompressedBodyData().Length} bytes");
        }

        [Benchmark]
        public void ParseHeader()
        {
            GameBoxFile.Parse(new MemoryStream(file));
        }

        [Benchmark]
        public void ParseBody()
        {
            this.parsedFile.ParseBody();
        }

        [Benchmark]
        public void ParseFullFile()
        {
            GameBoxFile.Parse(new MemoryStream(file)).ParseBody();
        }
    }
}
