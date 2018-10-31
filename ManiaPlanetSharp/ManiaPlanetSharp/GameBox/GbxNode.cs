using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxNode
        : IEnumerable<GbxChunk>
    {
        public GbxNode(int @class)
        {
            this.Class = @class;
        }

        public int Class { get; private set; }

        protected List<GbxChunk> Chunks { get; private set; } = new List<GbxChunk>();

        public GbxChunk this[int id]
        {
            get
            {
                GbxChunk result = this.Chunks.FirstOrDefault(chunk => chunk.Id == id) ?? this.Chunks.FirstOrDefault(chunk => chunk.Chunk == id);
                if (result == null)
                {
                    throw new KeyNotFoundException("No chunk matching the given id was found");
                }
                return result;
            }
        }

        public int Count { get => this.Chunks.Count; }

        public void Add(GbxChunk chunk)
        {
            this.Chunks.Add(chunk);
        }

        IEnumerator<GbxChunk> IEnumerable<GbxChunk>.GetEnumerator()
        {
            return Chunks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Chunks.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a gbx file chunk.
    /// </summary>
    public class GbxChunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GbxChunk"/> class.
        /// </summary>
        /// <param name="id">The chunk identifier.</param>
        /// <param name="length">The chunk size.</param>
        /// <param name="data">The chunk data.</param>
        public GbxChunk(int id, int length, byte[] data)
        {
            this.Id = id;
            this.Length = length;
            this.Data = data;
        }

        /// <summary>
        /// Returns the identifier of this chunk..
        /// </summary>
        /// <value>
        /// The chunk identifier.
        /// </value>
        public int Id { get; set; }

        public int Chunk { get => this.Id & 0xfff; }

        public int Class { get => (this.Id >> 12) & 0xfff; }

        public int Engine { get => (this.Id >> 24) & 0xff; }

        /// <summary>
        /// Returns the size of this chunk.
        /// </summary>
        /// <value>
        /// The size of this chunk.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Returns the version of this chunk.
        /// </summary>
        /// <value>
        /// The chunk version.
        /// </value>
        public int ChunkVersion
        {
            get
            {
                return (this.Data != null && this.Data.Length > 0) ? this.Data[0] : -1;
            }
        }

        /// <summary>
        /// Returns the data of this chunk.
        /// </summary>
        /// <value>
        /// The data of this chunk.
        /// </value>
        public byte[] Data { get; set; }

        public string GetDataString()
        {
            return Encoding.UTF8.GetString(this.Data);
        }

        public MemoryStream GetDataStream()
        {
            return new MemoryStream(this.Data);
        }
    }
}
