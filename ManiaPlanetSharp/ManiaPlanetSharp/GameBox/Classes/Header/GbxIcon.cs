using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxIcon
        : GbxClass
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public byte Unused { get; set; }
        public byte[] IconData { get; set; } //RGBA, 32bpp
    }

    public class GbxIconParser
        : GbxClassParser<GbxIcon>
    {
        protected override int ChunkId => 0x2E001004;

        protected override GbxIcon ParseChunkInternal(GbxReader chunk)
        {
            var result = new GbxIcon()
            {
                Width = chunk.ReadUInt16(),
                Height = chunk.ReadUInt16(),
                Unused = chunk.ReadByte()
            };
            result.IconData = chunk.ReadRaw(4 * result.Width * result.Height);
            return result;
        }
    }
}
