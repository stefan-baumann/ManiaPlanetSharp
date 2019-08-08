using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxBodyClassReferenceParser<TReferenceClass>
        : GbxClassParser<TReferenceClass>
        where TReferenceClass : GbxClass, new()
    {
        public GbxBodyClassReferenceParser(int chunk, GbxClassParser<TReferenceClass> parser)
        {
            this.chunk = chunk;
            this.Parser = parser;
        }

        private int chunk;
        protected override int ChunkId => this.chunk;

        protected GbxClassParser<TReferenceClass> Parser { get; private set; }

        protected override TReferenceClass ParseChunkInternal(GbxReader reader)
        {
            return this.Parser.ParseChunk(reader);
        }
    }
}
