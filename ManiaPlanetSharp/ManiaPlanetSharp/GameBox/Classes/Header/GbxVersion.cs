using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxVersionClass
        : GbxHeaderClass
    {
        public uint Version { get; set; }
    }

    public class GbxVersionClassParser
        : GbxHeaderClassParser<GbxVersionClass>
    {
        protected override int Chunk => 0x30430004;

        public override GbxVersionClass ParseChunk(GbxReader reader)
        {
            return new GbxVersionClass() { Version = reader.ReadUInt32() };
        }
    }
}
