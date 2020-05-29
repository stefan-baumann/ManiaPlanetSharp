using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Benchmark
{
    public class PreGeneratedParserBenchmark
        : ParserBenchmark
    {
        public override void Setup()
        {
            base.Setup();

            GlobalParserSettings.UseDynamicallyCompiledChunkParsers = false;
            GlobalParserSettings.UsePrecompiledParsersIfPresent = true;
            ParserFactory.ClearParsers();
            ParserFactory.InitializePrecompiledParsers();
        }
    }
}
