using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Base class for chunks.
    /// </summary>
    public abstract class Chunk
        : Node
    { }

    /// <summary>
    /// Special class for chunks of unknown type.
    /// </summary>
    public class UnknownChunk
        : Chunk
    {
        /// <summary>
        /// Creates a new instance of the <c>UnknownChunk</c> type.
        /// </summary>
        /// <param name="data">The raw data of the unknown chunk.</param>
        /// <param name="chunkId">The chunk id of the unknown chunk.</param>
        public UnknownChunk(byte[] data, uint chunkId)
            : this(data)
        {
            this.Id = chunkId;
        }

        /// <summary>
        /// Creates a new instance of the <c>UnknownChunk</c> type.
        /// </summary>
        /// <param name="data">The raw data of the unknown chunk.</param>
        public UnknownChunk(byte[] data)
        {
            this.Data = data;
        }
    }
}
