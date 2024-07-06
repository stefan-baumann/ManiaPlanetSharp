using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    /// <summary>
    /// Provides basic methods used for reading information from GameBox files.
    /// </summary>
    [DebuggerNonUserCode]
    public class GameBoxReader
        : IDisposable
    {
        public const uint EndMarkerClassId = 0xFACADE01;
        public const uint SkipMarker = 0x534B4950; //SKIP

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoxStreamReader"/> class.
        /// </summary>
        /// <param name="stream">The stream the GameBox data is read from.</param>
        public GameBoxReader(Stream stream, bool leaveOpen = false)
        {
            this.Stream = stream;
            this.Reader = new BinaryReader(stream, Encoding.Default, leaveOpen);
        }



        /// <summary>
        /// Returns the stream the gbx data is read from.
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Returns the reader used for reading information from the gbx data stream.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        protected BinaryReader Reader { get; private set; }

        /// <summary>
        /// Returns a boolean indicating whether this instance is currently parsing chunks in the gbx body or not
        /// </summary>
        public bool BodyMode { get; set; } = false;



        /// <summary>
        /// Skips the specified number of bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        public void Skip(int length)
        {
            this.Reader.ReadBytes(length);
        }

        /// <summary>
        /// Fetches raw data.
        /// </summary>
        /// <param name="length">The length of the raw data to be fetched in bytes.</param>
        public byte[] ReadRaw(int length)
        {
            return this.Reader.ReadBytes(length);
        }

        /// <summary>
        /// Reads a boolean.
        /// </summary>
        /// <returns><c>True</c>, if the value read is not equal to <c>0</c>.</returns>
        public bool ReadBool()
        {
            return this.Reader.ReadInt32() > 0;
        }

        /// <summary>
        /// Reads a byte.
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return this.Reader.ReadByte();
        }

        /// <summary>
        /// Reads a signed byte.
        /// </summary>
        /// <returns></returns>
        public sbyte ReadSByte()
        {
            return this.Reader.ReadSByte();
        }

        /// <summary>
        /// Reads a character.
        /// </summary>
        /// <returns>A <c>byte</c> read from the file casted to a <c>char</c>.</returns>
        public char ReadChar()
        {
            return (char)this.Reader.ReadByte();
        }

        /// <summary>
        /// Reads a 16-bit signed integer.
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            return this.Reader.ReadInt16();
        }

        /// <summary>
        /// Reads a 16-bit unsigned integer.
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16()
        {
            return this.Reader.ReadUInt16();
        }

        /// <summary>
        /// Reads a 32-bit signed integer.
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            return this.Reader.ReadInt32();
        }

        /// <summary>
        /// Reads a 32-bit unsigned integer.
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32()
        {
            return this.Reader.ReadUInt32();
        }

        /// <summary>
        /// Reads a 64-bit unsigned integer
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64()
        {
            return this.Reader.ReadUInt64();
        }

        /// <summary>
        /// Reads a 128-bit unsigned integer.
        /// </summary>
        /// <returns>An array with two <c>ulong</c> values which represent the 128-bit integer.</returns>
        public ulong[] ReadUInt128()
        {
            return new[] { this.ReadUInt64(), this.ReadUInt64() };
        }

        /// <summary>
        /// Reads a 32-bit floating point number.
        /// </summary>
        /// <returns></returns>
        public float ReadFloat()
        {
            return this.Reader.ReadSingle();
        }

        /// <summary>
        /// The maximum length of string that will be parsed automatically.
        /// </summary>
        public int MaxStringLength { get; set; } = 200000;

        /// <summary>
        /// Reads a string that is preceded by a 32-bit unsigned integer stating its length. This will throw a <c>InvalidDataException</c> for strings that exceed the length specified in <c>GameBoxReader.MaxStringLength</c> to prevent massive allocations caused by wrongly parsed strings. If you don't want this behaviour, use <c>GameBoxReader.ReadLongString()</c> instead.
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            uint length = this.ReadUInt32();
            if (length > this.MaxStringLength)
            {
                throw new InvalidDataException("The specified string length exceeds the specified maximum string length.");
            }

            return this.ReadString((int)length);
        }

        /// <summary>
        /// Reads a string of arbitrary length that may exceed <c>GameBoxReader.MaxStringLength</c>.
        /// </summary>
        /// <returns></returns>
        public string ReadLongString()
        {
            uint length = this.ReadUInt32();
            return this.ReadString((int)length);
        }

        //Pre-allocated buffer 
        private const int stringReadBufferLength = 256;
        private readonly byte[] stringReadBuffer = new byte[stringReadBufferLength];
        /// <summary>
        /// Reads a string of a specified length.
        /// </summary>
        /// <param name="length">The length of the string in bytes.</param>
        /// <returns></returns>
        public string ReadString(int length)
        {
            if (length == 0)
            {
                return null;
            }
            
            if (length <= stringReadBufferLength)
            {
                this.Reader.Read(stringReadBuffer, 0, length);
                return Encoding.UTF8.GetString(stringReadBuffer, 0, length);
            }
            else
            {
                return Encoding.UTF8.GetString(this.Reader.ReadBytes(length));
            }
        }

        private uint? LookbackStringVersion { get; set; }
        private List<string> LookbackStrings { get; set; } = new List<string>();
        /// <summary>
        /// Reads a lookback string relative to the context of this <c>GameBoxReader</c>.
        /// </summary>
        /// <returns></returns>
        public string ReadLookbackString()
        {
            if (LookbackStringVersion == null)
            {
                this.LookbackStringVersion = this.ReadUInt32();
            }

            uint index = this.ReadUInt32();
            if (((index & 0xc0000000) != 0 && (index & 0x3fffffff) == 0) || index == 0 /*|| (index & 0xc0000000) == 0*/)
            {
                string newString = this.ReadString();
                LookbackStrings.Add(newString);
                return newString;
            }
            if (index == uint.MaxValue)
            {
                return "Unassigned";
            }
            if ((index & 0x3fffffff) == index)
            {
                switch (index)
                {
                    case 0: return "Desert";
                    case 1: return "Snow";
                    case 2: return "Rally";
                    case 3: return "Island";
                    case 4: return "Bay";
                    case 5: return "Coast";
                    case 6: return "Stadium";
                    case 7: return "Basic";
                    case 8: return "Plain";
                    case 9: return "Moon";
                    case 10: return "Toy";
                    case 11: return "Valley";
                    case 12: return "Canyon";
                    case 13: return "Lagoon";
                    case 14: return "Deprecated_Arena";

                    case 17: return "TMCommon";
                    case 18: return "Canyon4";
                    case 19: return "Canyon256";
                    case 20: return "Valley4";
                    case 21: return "Valley256";
                    case 22: return "Lagoon4";
                    case 23: return "Lagoon256";
                    case 24: return "Stadium4";
                    case 25: return "Stadium256";
                    case 26: return "Trackmania";

                    case 100: return "History";
                    case 101: return "Society";
                    case 102: return "Galaxy";

                    case 200: return "Gothic";
                    case 201: return "Paris";
                    case 202: return "Storm";
                    case 203: return "Cryo";
                    case 204: return "Meteor";
                    case 205: return "Meteor4";
                    case 206: return "Meteor256";
                    case 299: return "SMCommon";

                    case 10000: return "Vehicles";
                    case 10001: return "Orbital";
                    case 10002: return "Actors";
                    case 10003: return "Common";

                    case uint.MaxValue: return "Unassigned";
                }
            }

            int storedIndex = (int)(index & 0x3fffffff);
            if (storedIndex > this.LookbackStrings.Count)
            {
                Debug.WriteLine($"Lookback String with Index {storedIndex} (0x{storedIndex:X8}) could not be found.");
                return $"[Unknown lookback string #{storedIndex}]";
            }
            else
            {
                return this.LookbackStrings[storedIndex - 1];
            }
        }

        /// <summary>
        /// Creates a new lookback string context temporarily until the <c>LookbackStringContext</c> instance is disposed.
        /// </summary>
        /// <returns></returns>
        public LookbackStringContext GetNewLookbackStringContext()
        {
            return new LookbackStringContext(this);
        }

        /// <summary>
        /// Returns a <c>GameBoxReader</c> instance with a shared lookbackstring context but a limited number of bytes it can parse. Bytes that are not parsed by the nested reader are still consumed by the outer reader.
        /// </summary>
        /// <param name="length">The number of bytes to consume from the outside and provide to the inside reader.</param>
        /// <returns></returns>
        public GameBoxReader GetNestedLengthLimitedReader(int length)
        {
            byte[] data = this.ReadRaw(length);
            MemoryStream ms = new MemoryStream(data);
            GameBoxReader nestedReader = new GameBoxReader(ms) { BodyMode = this.BodyMode, LookbackStrings = this.LookbackStrings, LookbackStringVersion = this.LookbackStringVersion, MaxStringLength = this.MaxStringLength, Nodes = this.Nodes };

            return nestedReader;
        }

        /// <summary>
        /// Reads a file reference
        /// </summary>
        /// <returns></returns>
        public FileReference ReadFileReference()
        {
            FileReference reference = new FileReference();
            reference.Version = this.ReadByte();
            if (reference.Version >= 3)
            {
                reference.Checksum = this.ReadRaw(32);
            }
            reference.FilePath = this.ReadString();
            if ((reference.FilePath?.Length > 0 && reference.Version >= 1) || reference.Version >= 3)
            {
                reference.LocatorUrl = this.ReadString();
            }

            return reference;
        }

        private Dictionary<uint, Node> Nodes { get; set; } = new Dictionary<uint, Node>();
        /// <summary>
        /// Reads a node reference.
        /// </summary>
        /// <returns></returns>
        public Node ReadNodeReference()
        {
            uint index = this.ReadUInt32();
            Debug.WriteLine($"    Found Node Reference with index {index} (0x{index:X8})");
            if (index == uint.MaxValue)
            {
                return null;
            }
            if (!this.Nodes.ContainsKey(index))
            {
                this.Nodes.Add(index, this.ReadBodyChunk());
            }
            return this.Nodes[index];
        }

        /// <summary>
        /// Reads a chunk from the body of a GameBox file.
        /// </summary>
        /// <returns></returns>
        public Chunk ReadBodyChunk()
        {
            if (!this.BodyMode)
            {
                throw new InvalidOperationException($"{nameof(ReadBodyChunk)} can only be performed on the body of a gbx file.");
            }

            uint id = this.ReadUInt32();
            for (; id == 0; id = this.ReadUInt32()) ;
            if (id == EndMarkerClassId)
            {
                return null;
            }
            if (ParserFactory.IsParseableChunkId(id))
            {
                if (ParserFactory.TryGetChunkParser(id, out var parser))
                {
                    //If skippable
                    if (parser.ParseableIds.First(p => p.Item1 == id).Item2)
                    {
                        if (this.ReadUInt32() != SkipMarker)
                        {
                            throw new InvalidDataException($"Expected skip marker in chunk with id 0x{id:X8}0x/{ClassIds.GetClassName(id & 0xFFFFF000)}.");
                        }
                        uint size = this.ReadUInt32();
                        var start = this.Stream.Position;
                        try
                        {
                            //this.lastChunkId = id;
                            var result = parser.Parse(this, id);
                            //this.lastChunkId = null;

                            if (this.Stream.Position != start + size)
                            {
                                throw new InvalidOperationException($"Reader for skippable chunk with id 0x{id:X8}0x/{ClassIds.GetClassName(id & 0xFFFFF000)} did not read the correct length.");
                            }

                            return result;
                        }
                        catch
                        {
                            this.Stream.Position = start + size;
                            throw;
                        }
                    }
                    else
                    {
                        return parser.Parse(this, id);
                    }
                }
            }
            throw new NotImplementedException($"Cannot parse chunk with id 0x{id:X8}0x/{ClassIds.GetClassName(id & 0xFFFFF000)}");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        protected bool TrySkipChunk(out UnknownChunk skipped)
        {
            try
            {
                uint skip = this.ReadUInt32();
                if (skip == SkipMarker)
                {
                    int length = (int)this.ReadUInt32();
                    skipped = new UnknownChunk(this.ReadRaw(length), SkipMarker);
                    return true;
                }
            }
            catch { }

            //Go back to position before parsing skip uint
            this.Stream.Position -= 4;
            skipped = null;
            return false;
        }



        /// <summary>
        /// Reads a 2d-vector consisting of two 32-bit floating point numbers.
        /// </summary>
        /// <returns></returns>
        public Vector2D ReadVec2D()
        {
            return new Vector2D(this.ReadFloat(), this.ReadFloat());
        }

        /// <summary>
        /// Reads a 3d-vector consisting of three 32-bit floating point numbers.
        /// </summary>
        /// <returns></returns>
        public Vector3D ReadVec3D()
        {
            return new Vector3D(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
        }

        /// <summary>
        /// Reads a 2d-size consisting of two 32-bit unsigned integers.
        /// </summary>
        /// <returns></returns>
        public Size2D ReadSize2D()
        {
            return new Size2D((int)this.ReadUInt32(), (int)this.ReadUInt32());
        }

        /// <summary>
        /// Reads a 3d-size consisting of three 32-bit unsigned integers.
        /// </summary>
        /// <returns></returns>
        public Size3D ReadSize3D()
        {
            return new Size3D((int)this.ReadUInt32(), (int)this.ReadUInt32(), (int)this.ReadUInt32());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
        public class LookbackStringContext
            : IDisposable
        {
            public LookbackStringContext(GameBoxReader reader)
            {
                this.Reader = reader ?? throw new ArgumentNullException(nameof(reader));

                this.LookbackStringVersion = this.Reader.LookbackStringVersion;
                this.LookbackStrings = this.Reader.LookbackStrings;

                this.Reader.LookbackStringVersion = null;
                this.Reader.LookbackStrings = new List<string>();
            }

            protected GameBoxReader Reader { get; private set; }

            protected uint? LookbackStringVersion { get; private set; }
            protected List<string> LookbackStrings { get; private set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
            public void Dispose()
            {
                this.Reader.LookbackStringVersion = this.LookbackStringVersion;
                this.Reader.LookbackStrings = this.LookbackStrings;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Reader.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
