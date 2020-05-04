using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Wrapper exception for exceptions that occur while parsing.
    /// </summary>
    public class ParseException
        : Exception
    {
        public ParseException(Exception innerException)
            : base("An internal error occured while trying to parse the gbx file", innerException)
        { }

        public ParseException()
            : base()
        { }

        public ParseException(string message)
            : base(message)
        { }

        public ParseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
