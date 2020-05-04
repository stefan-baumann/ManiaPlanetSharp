using ManiaPlanetSharp.GameBox.Parsing.ParserGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.CustomParserCodeGenerationTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine(ParserCodeGenerator.GenerateChunkParserString<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelChunk>());
        }
    }
}
