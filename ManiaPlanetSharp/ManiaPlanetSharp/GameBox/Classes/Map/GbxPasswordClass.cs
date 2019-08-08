using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxPasswordClass
        : GbxClass
    {
        public ulong[] Password { get; set; }
        public uint CRC { get; set; }
    }

    public class GbxPasswordClassParser
        : GbxClassParser<GbxPasswordClass>
    {
        protected override int ChunkId => 0x03043029;

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
