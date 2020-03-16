using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E001004)]
    public class CollectorIconChunk
        : Chunk
    {
        [Property]
        public ushort Width { get; set; }

        [Property]
        public ushort Height { get; set; }

        public Size Size => new Size(this.Width, this.Height);

        public int IconSize => this.Width * this.Height * 4;

        [Property, Array(nameof(IconSize))]
        public byte[] IconData { get; set; } //RGBA, 32bpp
    }
}
