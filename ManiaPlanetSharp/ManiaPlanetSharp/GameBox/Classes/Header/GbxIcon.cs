using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxIcon
        : GbxHeaderClass
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public byte Unused { get; set; }
        public byte[] IconData { get; set; } //RGBA, 32bpp
    }

    public class GbxIconParser
        : GbxHeaderClassParser<GbxIcon>
    {
        protected override int Chunk => 0x2E001004;

        public override GbxIcon ParseChunk(GbxReader chunk)
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
