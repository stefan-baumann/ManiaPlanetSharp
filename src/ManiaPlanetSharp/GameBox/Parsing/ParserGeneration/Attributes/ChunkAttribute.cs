using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ChunkAttribute
        : Attribute
    {
        public ChunkAttribute(uint id)
        {
            this.Id = id;
        }

        public uint Id { get; private set; }
    }
}
