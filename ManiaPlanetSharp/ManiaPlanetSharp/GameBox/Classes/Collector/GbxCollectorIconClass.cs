using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorIcon
        : GbxClass
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public byte Unused { get; set; }
        public byte[] IconData { get; set; } //RGBA, 32bpp
    }

    public class GbxCollectorIconParser
        : GbxClassParser<GbxCollectorIcon>
    {
        protected override int ChunkId => 0x2E001004;

        protected override GbxCollectorIcon ParseChunkInternal(GbxReader chunk)
        {
            var result = new GbxCollectorIcon()
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
