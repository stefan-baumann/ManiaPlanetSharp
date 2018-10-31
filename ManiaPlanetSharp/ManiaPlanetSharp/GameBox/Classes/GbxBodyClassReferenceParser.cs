using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxBodyClassReferenceParser<TReferenceClass>
        : GbxBodyClassParser<TReferenceClass>
        where TReferenceClass : GbxBodyClass, new()
    {
        public GbxBodyClassReferenceParser(int chunk, GbxBodyClassParser<TReferenceClass> parser)
        {
            this.chunk = chunk;
            this.Parser = parser;
        }

        private int chunk;
        protected override int Chunk => this.chunk;

        protected GbxBodyClassParser<TReferenceClass> Parser { get; private set; }

        protected override TReferenceClass ParseChunkInternal(GbxReader reader)
        {
            return this.Parser.ParseChunk(reader);
        }
    }
}
