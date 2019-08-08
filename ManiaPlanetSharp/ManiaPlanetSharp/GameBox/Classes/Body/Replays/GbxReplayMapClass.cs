using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxReplayMapClass
        : GbxBodyClass
    {
        public GbxReplayMapClass() { }

        public int Size { get; set; }
        public byte[] Map { get; set; }
    }

    public class GbxReplayMapClassParser
        : GbxBodyClassParser<GbxReplayMapClass>
    {
        protected override int Chunk => 0x3093002;

        protected override GbxReplayMapClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxReplayMapClass();
            result.Size = (int)reader.ReadUInt32();
            result.Map = reader.ReadRaw(result.Size);
            return result;
        }
    }
}
