using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxUnusedHeaderClass
        : GbxHeaderClass
    { }

    public class GbxUnusedHeaderClassParser
        : GbxHeaderClassParser<GbxUnusedHeaderClass>
    {
        public GbxUnusedHeaderClassParser(int chunk, Action<GbxReader> parser)
        {
            this.chunk = chunk;
            this.parser = parser;
        }

        private int chunk;
        protected override int Chunk => this.chunk;

        private Action<GbxReader> parser;
        public override GbxUnusedHeaderClass ParseChunk(GbxReader reader)
        {
            this.parser(reader);
            return new GbxUnusedHeaderClass();
        }
    }
}
