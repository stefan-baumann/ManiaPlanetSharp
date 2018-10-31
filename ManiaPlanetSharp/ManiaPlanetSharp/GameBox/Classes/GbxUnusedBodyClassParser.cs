﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxUnusedClass
        : GbxBodyClass
    { }

    public class GbxUnusedBodyClassParser
        : GbxBodyClassParser<GbxUnusedClass>
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
        protected override int Chunk => this.chunk;

        private bool skippable = false;
        public override bool Skippable => this.skippable;

        private Action<GbxReader> parser;
        protected override GbxUnusedClass ParseChunkInternal(GbxReader reader)
        {
            this.parser(reader);
            return new GbxUnusedClass();
        }
    }
}