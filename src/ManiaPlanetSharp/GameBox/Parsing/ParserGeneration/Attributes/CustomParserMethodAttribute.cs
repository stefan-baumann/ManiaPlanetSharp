using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CustomParserMethodAttribute
        : Attribute
    {
        public CustomParserMethodAttribute(string parserMethod)
        {
            this.ParserMethod = parserMethod;
        }

        public string ParserMethod { get; set; }
    }
}
