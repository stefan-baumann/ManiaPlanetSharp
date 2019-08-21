using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorIcon
        : Node
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public Size Size => new Size(this.Width, this.Height);
        public byte[] IconData { get; set; } //RGBA, 32bpp
    }

    public class GbxCollectorIconParser
        : ClassParser<GbxCollectorIcon>
    {
        protected override int ChunkId => 0x2E001004;

        protected override GbxCollectorIcon ParseChunkInternal(GameBoxReader chunk)
        {
            var result = new GbxCollectorIcon()
            {
                Width = chunk.ReadUInt16(),
                Height = chunk.ReadUInt16()
            };
            result.IconData = chunk.ReadRaw(4 * result.Width * result.Height);
            return result;
        }
    }
}
