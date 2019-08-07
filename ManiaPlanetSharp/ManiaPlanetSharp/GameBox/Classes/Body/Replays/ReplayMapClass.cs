using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class ReplayMapClass
        : GbxBodyClass
    {
        public ReplayMapClass() { }

        public int Size { get; set; }
        public byte[] Map { get; set; }
    }

    public class ReplayMapClassParser
        : GbxBodyClassParser<ReplayMapClass>
    {
        protected override int Chunk => 0x3093002;

        protected override ReplayMapClass ParseChunkInternal(GbxReader reader)
        {
            var result = new ReplayMapClass();
            result.Size = (int)reader.ReadUInt32();
            result.Map = reader.ReadRaw(result.Size);
            return result;
        }
    }
}
