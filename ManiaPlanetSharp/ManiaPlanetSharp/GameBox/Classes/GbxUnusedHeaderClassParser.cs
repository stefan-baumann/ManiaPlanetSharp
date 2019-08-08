using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxUnusedHeaderClass
        : GbxClass
    { }

    public class GbxUnusedHeaderClassParser
        : GbxClassParser<GbxUnusedHeaderClass>
    {
        public GbxUnusedHeaderClassParser(int chunk, Action<GbxReader> parser)
        {
            this.chunk = chunk;
            this.parser = parser;
        }

        private int chunk;
        protected override int ChunkId => this.chunk;

        private Action<GbxReader> parser;
        protected override GbxUnusedHeaderClass ParseChunkInternal(GbxReader reader)
        {
            this.parser(reader);
            return new GbxUnusedHeaderClass();
        }
    }
}
