using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxOldPasswordClass
        : Node
    {
        public uint Unused { get; set; }
        public string Password { get; set; }
    }

    public class GbxOldPasswordClassParser
        : ClassParser<GbxOldPasswordClass>
    {
        protected override int ChunkId => 0x03043014;

        public override bool Skippable => true;

        protected override GbxOldPasswordClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxOldPasswordClass()
            {
                Unused = reader.ReadUInt32(),
                Password = reader.ReadString()
            };
        }
    }
}
