using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxUnusedClass
        : GbxClass
    { }

    public class GbxUnusedClassParser
        : GbxClassParser<GbxUnusedClass>
    {
        public GbxUnusedClassParser(int chunk, Action<GbxReader> parser)
            : this(chunk, false, parser)
        { }

        public GbxUnusedClassParser(int chunk, bool skippable, Action<GbxReader> parser)
        {
            this.chunk = chunk;
            this.skippable = skippable;
            this.parser = parser;
        }

        private readonly int chunk;
        protected override int ChunkId => this.chunk;

        private readonly bool skippable = false;
        public override bool Skippable => this.skippable;

        private readonly Action<GbxReader> parser;
        protected override GbxUnusedClass ParseChunkInternal(GbxReader reader)
        {
            this.parser(reader);
            return new GbxUnusedClass();
        }
    }
}