using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class Chunk
        : Node
    { }

    public class UnknownChunk
        : Chunk
    {
        public UnknownChunk(byte[] data, uint chunkId)
            : this(data)
        {
            this.Id = chunkId;
        }

        public UnknownChunk(byte[] data)
        {
            this.Data = data;
        }
    }
}
