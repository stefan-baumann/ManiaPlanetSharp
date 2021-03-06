﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ChunkAttribute
        : Attribute
    {
        public ChunkAttribute(uint id)
        {
            this.Id = id;
        }
        public ChunkAttribute(uint id, bool skippable)
            : this(id)
        {
            this.Skippable = skippable;
        }

        public uint Id { get; private set; }

        public bool Skippable { get; set; }
    }
}
