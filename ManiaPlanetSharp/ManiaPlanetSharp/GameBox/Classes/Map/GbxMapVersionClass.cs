using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxMapVersionClass
        : Node
    {
        public uint Version { get; set; }
    }

    public class GbxVersionClassParser
        : ClassParser<GbxMapVersionClass>
    {
        protected override int ChunkId => 0x3043004;

        protected override GbxMapVersionClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxMapVersionClass() { Version = reader.ReadUInt32() };
        }
    }
}
