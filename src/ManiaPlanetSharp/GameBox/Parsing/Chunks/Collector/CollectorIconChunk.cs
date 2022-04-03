using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E001004)]
    public class CollectorIconChunk
        : Chunk
    {
        [Property]
        public byte Width { get; set; }

        [Property]
        public byte Unknown1 { get; set; }

        [Property]
        public byte Height { get; set; }

        [Property]
        public byte Unknown2 { get; set; }

        public byte[] Padding { get; set; }

        public Size Size => new Size(this.Width, this.Height);

        public int IconSize => this.Width * this.Height * 4;

        [Property, CustomParserMethod(nameof(ReadIconData))]
        public byte[] IconData { get; set; } //RGBA 32bpp or WebP

        public byte[] ReadIconData(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            // Both of these are 0 for the old, uncompressed collector images, and 128 for the new webp ones
            if (this.Unknown1 == 0 && this.Unknown2 == 0)
            {
                return reader.ReadRaw(this.IconSize);
            }

            if (this.Unknown1 != 128 || this.Unknown2 != 128)
            {
                Console.WriteLine($"Unknown collector image flag values {{ {this.Unknown1}, {this.Unknown2} }}. Attempting to parse a WebP icon...");
            }

            // Parse WebP data
            // There are some bytes before the typical WebP header starts, just search for the magic string and skip forwards to it.
            var remainingDataStart = reader.Stream.Position;
            var offset = 0;
            for (int i = 0; reader.Stream.Length - reader.Stream.Position - 4 > 0; i++)
            {
                reader.Stream.Position = remainingDataStart + i;
                uint u = reader.ReadUInt32();
                if (u == 0x46464952) // "RIFF"
                {
                    offset = i;
                    break;
                }
            }

            // Read the actual WebP data
            reader.Stream.Position = remainingDataStart + offset;
            int length = (int)(reader.Stream.Length - reader.Stream.Position);
            return reader.ReadRaw(length);
        }
    }
}
