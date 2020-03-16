using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class Chunk
        : Node
    {
    }

    public class UnknownChunk
        : Chunk
    {
        public UnknownChunk(byte[] rawData)
        {
            this.RawData = rawData;
        }

        public byte[] RawData { get; set; }
    }
}
