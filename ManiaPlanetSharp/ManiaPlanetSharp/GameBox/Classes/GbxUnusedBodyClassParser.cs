using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxUnusedBodyClass
        : GbxClass
    { }

    public class GbxUnusedBodyClassParser
        : GbxClassParser<GbxUnusedBodyClass>
    {
        public GbxUnusedBodyClassParser(int chunk, Action<GbxReader> parser)
            : this(chunk, false, parser)
        { }

        public GbxUnusedBodyClassParser(int chunk, bool skippable, Action<GbxReader> parser)
        {
            this.chunk = chunk;
            this.skippable = skippable;
            this.parser = parser;
        }

        private int chunk;
        protected override int ChunkId => this.chunk;

        private bool skippable = false;
        public override bool Skippable => this.skippable;

        private Action<GbxReader> parser;
        protected override GbxUnusedBodyClass ParseChunkInternal(GbxReader reader)
        {
            this.parser(reader);
            return new GbxUnusedBodyClass();
        }
    }
}