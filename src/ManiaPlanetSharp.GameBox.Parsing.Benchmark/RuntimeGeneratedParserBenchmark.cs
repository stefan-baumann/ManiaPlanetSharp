using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Benchmark
{
    public class RuntimeGeneratedParserBenchmark
        : ParserBenchmark
    {
        public override void Setup()
        {
            base.Setup();

            ParserFactory.ClearParsers();
            GlobalParserSettings.UseDynamicallyCompiledChunkParsers = true;
            GlobalParserSettings.UsePrecompiledParsersIfPresent = false;
            ParserFactory.ClearParsers();
            ParserFactory.ScanForParseableChunks();
        }
    }
}
