using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    public static class GlobalParserSettings
    {
        public static uint MaximumUncompressedGameBoxBodySize { get; set; } = 100000000; //100MB

        private static bool usePrecompiledParsers = true;
        public static bool UsePrecompiledParsers
        {
            get
            {
                return usePrecompiledParsers;
            }
            set
            {
                if (value != UsePrecompiledParsers)
                {
                    usePrecompiledParsers = value;
                    if (UsePrecompiledParsers)
                    {
                        ParserFactory.InitializePrecompiledParsers();
                    }
                    else
                    {
                        ParserFactory.ClearParsers();
                    }
                }
            }
        }
    }
}
