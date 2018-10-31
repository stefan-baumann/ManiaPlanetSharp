using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxPasswordClass
        : GbxBodyClass
    {
        public ulong[] Password { get; set; }
        public uint CRC { get; set; }
    }

    public class GbxPasswordClassParser
        : GbxBodyClassParser<GbxPasswordClass>
    {
        protected override int Chunk => 0x03043029;

        protected override GbxPasswordClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxPasswordClass()
            {
                Password = reader.ReadUInt128(),
                CRC = reader.ReadUInt32()
            };
        }
    }
}
