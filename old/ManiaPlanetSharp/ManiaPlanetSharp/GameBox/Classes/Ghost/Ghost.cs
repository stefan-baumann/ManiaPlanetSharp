using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class Ghost
        : Node
    {
        public uint IsReplaying { get; set; }
        public uint UncompressedSize { get; set; }
        public uint CompressedSize { get; set; }
        public byte[] CompressedData { get; set; }
    }

    public class GhostParserA
        : ClassParser<Ghost>
    {
        protected override int ChunkId => 0x0303F005;

        protected override Ghost ParseChunkInternal(GameBoxReader reader)
        {
            Ghost result = new Ghost();
            result.UncompressedSize = reader.ReadUInt32();
            result.CompressedSize = reader.ReadUInt32();
            result.CompressedData = reader.ReadRaw((int)result.CompressedSize);
            return result;
        }
    }

    public class GhostParserB
        : ClassParser<Ghost>
    {
        protected override int ChunkId => 0x0303F006;

        protected override Ghost ParseChunkInternal(GameBoxReader reader)
        {
            uint isReplaying = reader.ReadUInt32();
            var result = new GhostParserA().ParseChunk(reader);
            result.IsReplaying = isReplaying;
            return result;
        }
    }
}
