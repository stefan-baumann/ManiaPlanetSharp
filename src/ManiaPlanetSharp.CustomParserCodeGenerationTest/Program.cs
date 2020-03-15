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
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine(ParserCodeGenerator2.GenerateChunkParserString<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelChunk>());
        }
    }
}
