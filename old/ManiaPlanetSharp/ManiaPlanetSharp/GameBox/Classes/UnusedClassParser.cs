using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class UnusedClass
        : Node
    { }

    public class UnusedClassParser
        : ClassParser<UnusedClass>
    {
        public UnusedClassParser(int chunk, Action<GameBoxReader> parser)
            : this(chunk, false, parser)
        { }

        public UnusedClassParser(int chunk, bool skippable, Action<GameBoxReader> parser)
        {
            this.chunk = chunk;
            this.skippable = skippable;
            this.parser = parser;
        }

        private readonly int chunk;
        protected override int ChunkId => this.chunk;

        private readonly bool skippable = false;
        public override bool Skippable => this.skippable;

        private readonly Action<GameBoxReader> parser;
        protected override UnusedClass ParseChunkInternal(GameBoxReader reader)
        {
            this.parser(reader);
            return new UnusedClass();
        }
    }
}