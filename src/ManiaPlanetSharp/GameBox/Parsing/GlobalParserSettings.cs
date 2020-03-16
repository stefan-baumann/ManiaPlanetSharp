using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    public static class GlobalParserSettings
    {
        public static uint MaximumUncompressedGameBoxBodySize { get; } = 100000000; //100MB
    }
}
