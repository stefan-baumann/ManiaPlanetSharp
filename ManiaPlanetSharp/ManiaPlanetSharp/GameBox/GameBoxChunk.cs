using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Represents a gbx file chunk.
    /// </summary>
    public class GameBoxChunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoxChunk"/> class.
        /// </summary>
        /// <param name="id">The chunk identifier.</param>
        /// <param name="length">The chunk size.</param>
        /// <param name="data">The chunk data.</param>
        public GameBoxChunk(int id, int length, byte[] data)
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
        public int Id { get; private set; }

        /// <summary>
        /// Returns the size of this chunk.
        /// </summary>
        /// <value>
        /// The size of this chunk.
        /// </value>
        public int Length { get; private set; }

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
                return this.Data != null ? this.Data[0] : -1;
            }
        }

        /// <summary>
        /// Returns the data of this chunk.
        /// </summary>
        /// <value>
        /// The data of this chunk.
        /// </value>
        public byte[] Data { get; set; }

        public string DataString
        {
            get
            {
                return Encoding.UTF8.GetString(this.Data);
            }
        }
    }
}
