using System;
using System.Runtime.Serialization;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    [Serializable]
    internal class ParserGeneratorException : Exception
    {
        public ParserGeneratorException()
        { }

        public ParserGeneratorException(string message)
            : base(message)
        { }

        public ParserGeneratorException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected ParserGeneratorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}