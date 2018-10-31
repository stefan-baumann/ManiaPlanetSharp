using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxOldPasswordClass
        : GbxBodyClass
    {
        public uint Unused { get; set; }
        public string Password { get; set; }
    }

    public class GbxOldPasswordClassParser
        : GbxBodyClassParser<GbxOldPasswordClass>
    {
        protected override int Chunk => 0x03043014;

        public override bool Skippable => true;

        protected override GbxOldPasswordClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxOldPasswordClass()
            {
                Unused = reader.ReadUInt32(),
                Password = reader.ReadString()
            };
        }
    }
}
