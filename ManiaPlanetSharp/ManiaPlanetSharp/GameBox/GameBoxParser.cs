using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Provides basic functionality for parsing gbx files.
    /// </summary>
    /// <typeparam name="TOut">The output data type.</typeparam>
    public abstract class GameBoxParser<TOut>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GbxParserBase"/> class.
        /// </summary>
        /// <param name="stream">The gbx file stream.</param>
        protected GameBoxParser(Stream stream)
        {
            this.Reader = new GameBoxStreamReader(stream);
            this.ParseInternal();
        }

        /// <summary>
        /// Returns the reader used for reading the gbx file.
        /// </summary>
        /// <value>
        /// The reader used for reading the gbx file.
        /// </value>
        protected internal GameBoxStreamReader Reader { get; private set; }

        /// <summary>
        /// Returns the version of this gbx file.
        /// </summary>
        /// <value>
        /// The gbx file version.
        /// </value>
        protected internal short Version { get; private set; }

        /// <summary>
        /// Returns the header storage.
        /// </summary>
        /// <value>
        /// The header storage.
        /// </value>
        protected internal byte[] HeaderStorage { get; private set; }

        /// <summary>
        /// Returns the gbx class identifier.
        /// </summary>
        /// <value>
        /// The gbx class identifier.
        /// </value>
        protected internal int ClassId { get; private set; }

        /// <summary>
        /// Returns the length of the gbx file header.
        /// </summary>
        /// <value>
        /// The length of the gbx file header.
        /// </value>
        protected internal int HeaderLength { get; private set; }

        /// <summary>
        /// Returns a list of all the chunks in this gbx file.
        /// </summary>
        /// <value>
        /// The chunks in this gbx file.
        /// </value>
        public List<GameBoxChunk> Chunks { get; private set; }

        //public GameBoxChunk Body { get; private set; }

        /// <summary>
        /// Parses the supplied gbx file.
        /// </summary>
        /// <returns>The data parsed from the supplied gbx file.</returns>
        public abstract TOut Parse();

        /// <summary>
        /// Parses the header.
        /// </summary>
        /// <exception cref="System.IO.InvalidDataException">The gbx file does not start with the string 'GBX'.</exception>
        protected virtual void ParseInternal()
        {
            //Check for the start sequence 'GBX'
            if (Encoding.UTF8.GetString(this.Reader.ReadRaw(3)) != "GBX")
            {
                throw new InvalidDataException("The gbx file does not start with the string 'GBX'.");
            }

            try
            {
                //Read general metrics
                this.Version = this.Reader.ReadShort();
                this.HeaderStorage = this.Reader.ReadRaw(4);
                this.ClassId = this.Reader.ReadLong();
                this.HeaderLength = this.Reader.ReadLong();
                
                //Read all the chunk metrics
                this.Chunks = new List<GameBoxChunk>();
                for (int chunks = this.Reader.ReadLong(); chunks > 0; chunks--)
                {
                    this.Chunks.Add(new GameBoxChunk(this.Reader.ReadLong(), this.Reader.ReadLong() & 0x7fffffff, null));
                }
                this.Chunks = this.Chunks.OrderBy(chunk => chunk.Id).ToList();

                //Read all the chunks
                foreach (GameBoxChunk chunk in this.Chunks)
                {
                    chunk.Data = this.Reader.ReadRaw(chunk.Length);
                }
                


                ////Read body metrics
                //this.Reader.Skip(8);
                //int decompressedLength = this.Reader.ReadLong();
                //int compressedLength = this.Reader.ReadLong();
                
                ////Read and decompress body
                //byte[] bodyCompressed = this.Reader.ReadRaw(compressedLength);
                //byte[] bodyDecompressed = new Utils.LzoCompressor().Decompress(bodyCompressed, decompressedLength);
                //this.Body = new Chunk(-1, decompressedLength, bodyDecompressed);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("The supplied gbx file is invalid.", ex);
            }
        }
    }
}
