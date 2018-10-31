using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxVersionClass
        : GbxChallengeClass
    {
        public uint Version { get; set; }
    }

    public class GbxVersionClassParser
        : GbxChallengeClassParser<GbxVersionClass>
    {
        protected override int Chunk => 4;

        protected override GbxVersionClass ParseChunk(GbxReader reader)
        {
            return new GbxVersionClass() { Version = reader.ReadUInt32() };
        }
    }
}
