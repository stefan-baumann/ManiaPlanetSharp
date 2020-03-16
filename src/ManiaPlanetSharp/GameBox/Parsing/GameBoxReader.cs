using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Provides basic methods used for reading information from .gbx files.
    /// </summary>
    [DebuggerNonUserCode]
    public class GameBoxReader
        : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoxStreamReader"/> class.
        /// </summary>
        /// <param name="stream">The stream the gbx data should be read from.</param>
        public GameBoxReader(Stream stream)
        {
            this.Stream = stream;
            this.Reader = new BinaryReader(stream);
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
        /// <param name="length">The length.</param>
        public byte[] ReadRaw(int length)
        {
            return this.Reader.ReadBytes(length);
        }

        public bool ReadBool()
        {
            return this.Reader.ReadInt32() > 0;
        }

        public byte ReadByte()
        {
            return this.Reader.ReadByte();
        }

        public char ReadChar()
        {
            return (char)this.Reader.ReadByte();
        }

        public ushort ReadUInt16()
        {
            return this.Reader.ReadUInt16();
        }

        public int ReadInt32()
        {
            return this.Reader.ReadInt32();
        }

        public uint ReadUInt32()
        {
            return this.Reader.ReadUInt32();
        }

        public ulong ReadUInt64()
        {
            return this.Reader.ReadUInt64();
        }

        public ulong[] ReadUInt128()
        {
            return new[] { this.ReadUInt64(), this.ReadUInt64() };
        }

        public float ReadFloat()
        {
            return this.Reader.ReadSingle();
        }

        public string ReadString()
        {
            uint length = this.ReadUInt32();
            if (length == 0)
            {
                return null;
            }

            return this.ReadString((int)length);
        }

        public string ReadString(int length)
        {
            byte[] data = this.Reader.ReadBytes(length);
            return Encoding.UTF8.GetString(data);
        }

        private uint? LookbackStringVersion { get; set; }
        private List<string> LookbackStrings { get; set; } = new List<string>();
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
            if (index == uint.MaxValue) //?
            {
                return string.Empty;
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
                return null;
            }
            else
            {
                return this.LookbackStrings[storedIndex - 1];
            }
        }

        public void ResetLocalLbsStrings()
        {
            this.LookbackStrings.Clear();
        }

        public FileReference ReadFileReference()
        {
            FileReference reference = new FileReference();
            reference.Version = this.ReadByte();
            if (reference.Version >= 3)
            {
                reference.Checksum = this.ReadRaw(32);
            }
            reference.FilePath = this.ReadString();
            if (reference.FilePath.Length > 0 && reference.Version >= 1)
            {
                reference.LocatorUrl = this.ReadString();
            }

            return reference;
        }

        private Dictionary<uint, Node> Nodes { get; set; } = new Dictionary<uint, Node>();
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
                this.Nodes.Add(index, this.ReadNode());
            }
            return this.Nodes[index];
        }

        public Node ReadNode()
        {
            //new NodeParser().ParseNode(this);
            throw new NotImplementedException();
        }



        public Vector2D ReadVec2D()
        {
            return new Vector2D(this.ReadFloat(), this.ReadFloat());
        }

        public Vector3D ReadVec3D()
        {
            return new Vector3D(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
        }

        public Size2D ReadSize2D()
        {
            return new Size2D((int)this.ReadUInt32(), (int)this.ReadUInt32());
        }

        public Size3D ReadSize3D()
        {
            return new Size3D((int)this.ReadUInt32(), (int)this.ReadUInt32(), (int)this.ReadUInt32());
        }

        public void Dispose()
        {
            this.Reader.Dispose();
        }
    }
}
