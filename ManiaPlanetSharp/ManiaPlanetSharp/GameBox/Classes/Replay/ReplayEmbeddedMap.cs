using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Replay
{
    public class ReplayEmbeddedMap
        : Node
    {
        public ReplayEmbeddedMap() { }

        public int Size { get; set; }
        public byte[] Map { get; set; }
    }

    public class ReplayEmbeddedMapParser
        : ClassParser<ReplayEmbeddedMap>
    {
        protected override int ChunkId => 0x3093002;

        protected override ReplayEmbeddedMap ParseChunkInternal(GameBoxReader reader)
        {
            var result = new ReplayEmbeddedMap();
            result.Size = (int)reader.ReadUInt32();
            result.Map = reader.ReadRaw(result.Size);
            return result;
        }
    }
}
