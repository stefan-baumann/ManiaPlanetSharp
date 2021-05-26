using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Base class for chunks.
    /// </summary>
    public abstract class Chunk
        : Node
    {
        /// <summary>
        /// The full id of this chunk.
        /// </summary>
        public override uint Id { get; set; }

        /// <summary>
        /// The class part of the id of this chunk.
        /// </summary>
        public virtual uint ClassId => this.Id & 0xFFFFF000;

        /// <summary>
        /// The chunk part of the id of this chunk.
        /// </summary>
        public virtual uint ChunkId => this.Id & 0xFFF;

        public override List<Chunk> Chunks => new List<Chunk>() { this };

        /// <summary>
        /// The raw data of this chunk.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Creates a <c>MemoryStream</c> with the raw data of this node.
        /// </summary>
        /// <returns></returns>
        public virtual Stream GetStream()
        {
            return new MemoryStream(this.Data ?? throw new NullReferenceException("This node does not have any stored data."));
        }



        /// <summary>
        /// Returns a string that represents the current node.
        /// </summary>
        public override string ToString()
        {
            return $"{this.GetType().Name} (0x{this.Id:X8}/{this.GetClassName()})";
        }
    }

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
