using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E002016)]
    public class ObjectDefaultSkinChunk
        : Chunk
    {
        [Property]
        public FileReference DefaultSkin { get; set; } //Might be node
    }
}
