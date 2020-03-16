using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class ParseException
        : Exception
    {
        public ParseException(Exception innerException)
            : base("An internal error occured while trying to parse the gbx file", innerException)
        { }
    }
}
