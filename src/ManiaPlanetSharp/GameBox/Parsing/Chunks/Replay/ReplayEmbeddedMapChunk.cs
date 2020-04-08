using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093002)]
    public class ReplayEmbeddedMapChunk
        : Chunk
    {
        [Property, Array]
        public byte[] MapData { get; set; }

        public GameBoxFile ParseMap()
        {
            if (this.MapData == null)
            {
                throw new ArgumentNullException(nameof(MapData));
            }
            if (this.MapData.Length == 0)
            {
                return new GameBoxFile();
            }

            using (MemoryStream stream = new MemoryStream(this.MapData))
            {
                return GameBoxFile.Parse(stream);
            }
        }
    }
}
