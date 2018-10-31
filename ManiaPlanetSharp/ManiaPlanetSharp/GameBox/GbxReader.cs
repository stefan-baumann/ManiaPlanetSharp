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
    public class GbxReader
        : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoxStreamReader"/> class.
        /// </summary>
        /// <param name="stream">The stream the gbx data should be read from.</param>
        public GbxReader(Stream stream)
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
                return string.Empty;
            }

            return this.ReadString((int)length);
        }

        public string ReadString(int length)
        {
            byte[] data = this.Reader.ReadBytes(length);
            return Encoding.UTF8.GetString(data);
        }

        private uint? LbsVersion { get; set; }
        private List<string> LbsStrings { get; set; } = new List<string>();
        public string ReadLoopbackString()
        {
            if (LbsVersion == null)
            {
                this.LbsVersion = this.ReadUInt32();
            }

            uint index = this.ReadUInt32();
            if (((index & 0xc0000000) != 0 && (index & 0x3fffffff) == 0) || index == 0)
            {
                string newString = this.ReadString();
                LbsStrings.Add(newString);
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
                    case 11: return "Valley";
                    case 12: return "Canyon";
                    case 17: return "TMCommon";
                    case 202: return "Storm";
                    case 299: return "SMCommon";
                    case 10003: return "Common";
                }
            }

            int storedIndex = (int)(index & 0x3fffffff);
            if (storedIndex >= this.LbsStrings.Count)
            {
                Debug.WriteLine($"Loopback String with Index {storedIndex} could not be found.");
                return string.Empty;
            }
            else
            {
                return this.LbsStrings[storedIndex];
            }
        }

        public void ResetLocalLbsStrings()
        {
            this.LbsStrings.Clear();
        }

        public GbxFileReference ReadFileRef()
        {
            GbxFileReference reference = new GbxFileReference();
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

        private Dictionary<uint, GbxNode> Nodes { get; set; } = new Dictionary<uint, GbxNode>();
        public GbxNode ReadNodeReference()
        {
            uint index = this.ReadUInt32();
            Debug.WriteLine($"    Found Node Reference with index {index} (0x{index:X8})");
            if (index == uint.MaxValue)
            {
                return null;
            }
            if (!this.Nodes.ContainsKey(index))
            {
                //this.Nodes.Add(index, new GbxNodeParser().ParseSingleNode(this));
                this.Nodes.Add(index, new GbxNodeParser().ParseNode(this));
            }
            return this.Nodes[index];
        }



        public Vec2D ReadVec2D()
        {
            return new Vec2D(this.ReadFloat(), this.ReadFloat());
        }

        public Vec3D ReadVec3D()
        {
            return new Vec3D(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
        }

        public Color ReadColor()
        {
            return new Color(this.ReadFloat(), this.ReadFloat(), this.ReadFloat());
        }

        public void Dispose()
        {
            this.Reader.Dispose();
        }
    }
}
