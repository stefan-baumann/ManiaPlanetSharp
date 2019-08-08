using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class ClassReferenceParser<TReferenceClass>
        : ClassParser<TReferenceClass>
        where TReferenceClass : Node, new()
    {
        public ClassReferenceParser(int chunk, ClassParser<TReferenceClass> parser)
        {
            this.chunk = chunk;
            this.Parser = parser;
        }

        private readonly int chunk;
        protected override int ChunkId => this.chunk;

        protected ClassParser<TReferenceClass> Parser { get; private set; }

        protected override TReferenceClass ParseChunkInternal(GameBoxReader reader)
        {
            return this.Parser.ParseChunk(reader);
        }
    }
}
