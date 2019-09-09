using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002010)]
    public class ObjectBannerProfileChunk
        : Chunk
    {
        [Property]
        public FileReference BannerProfile { get; set; }
    }
}
