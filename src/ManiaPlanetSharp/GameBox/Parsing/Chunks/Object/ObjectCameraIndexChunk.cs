using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002006)]
    public class ObjectCameraIndexChunk
        : Chunk
    {
        [Property]
        public uint DefaultCameraIndex { get; set; }
    }
}
