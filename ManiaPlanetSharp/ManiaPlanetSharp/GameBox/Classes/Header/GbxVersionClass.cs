using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxVersionClass
        : GbxClass
    {
        public uint Version { get; set; }
    }

    public class GbxVersionClassParser
        : GbxClassParser<GbxVersionClass>
    {
        protected override int ChunkId => 0x3043004;

        protected override GbxVersionClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxVersionClass() { Version = reader.ReadUInt32() };
        }
    }
}
