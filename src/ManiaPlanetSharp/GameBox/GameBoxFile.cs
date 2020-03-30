﻿using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    [CustomStruct]
    public class GameBoxFile
    {
        public static GameBoxFile Parse(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                return GameBoxFile.Parse(stream);
            }
        }

        public static GameBoxFile Parse(Stream stream)
        {
            using (GameBoxReader reader = new GameBoxReader(stream))
            {
#if !DEBUG
                try
                {
#endif
                    return ParserFactory.GetCustomStructParser<GameBoxFile>().Parse(reader);
#if !DEBUG
                }
                catch (Exception ex)
                {
                    throw new ParseException(ex);
                }
#endif
            }
        }

#region Header

        [Property, CustomParserMethod(nameof(ReadMagicString))]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string MagicString { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ReadMagicString(GameBoxReader reader)
        {
            string magicString = reader.ReadString(3);
            if (magicString != "GBX")
            {
                throw new ArgumentException("The magic string is invalid.", nameof(MagicString));
            }
            return magicString;
        }

        [Property]
        public ushort Version { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 3)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public char FileFormatC { get; set; }
        public FileFormat FileFormat => this.FileFormatC == 'T' ? FileFormat.Text : FileFormat.Binary;

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 3)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public char ReferenceTableCompressedC { get; set; }
        public bool ReferenceTableCompressed => this.ReferenceTableCompressedC == 'C';

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 3)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public char BodyCompressedC { get; set; }
        public bool BodyCompressed => this.BodyCompressedC == 'C';

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 4)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public char Unused { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 3)]
        public uint MainClassID { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6)]
        public uint UserDataSize { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6), Condition(nameof(UserDataSize), ConditionOperator.GreaterThan, 0)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public uint HeaderChunkCount { get; set; }

        [Property, CustomParserMethod(nameof(ParseHeaderChunkEntries)), Condition(nameof(HeaderChunkCount), ConditionOperator.GreaterThan, 0)]
        public HeaderEntry[] HeaderChunkEntries { get; set; } = Array.Empty<HeaderEntry>();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HeaderEntry[] ParseHeaderChunkEntries(GameBoxReader reader)
        {
            List<HeaderEntry> headerEntries = new List<HeaderEntry>((int)this.HeaderChunkCount);
            CustomStructParser<HeaderEntry> headerEntryParser = ParserFactory.GetCustomStructParser<HeaderEntry>();
            for (int i = 0; i < this.HeaderChunkCount; i++)
            {
                headerEntries.Add(headerEntryParser.Parse(reader));
            }
            return headerEntries.OrderBy(entry => entry.ChunkID).ToArray();
        }

        [Property, CustomParserMethod(nameof(ParseHeaderChunks)), Condition(nameof(HeaderChunkCount), ConditionOperator.GreaterThan, 0)]
        public Node[] HeaderChunks { get; set; } = Array.Empty<Node>();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Node[] ParseHeaderChunks(GameBoxReader reader)
        {
            Node[] headerChunks = new Node[this.HeaderChunkCount];
            CustomStructParser<HeaderEntry> headerEntryParser = ParserFactory.GetCustomStructParser<HeaderEntry>();
            long start = reader.Stream.Position;
            for (uint i = 0, offset = 0; i < this.HeaderChunkCount; i++)
            {
                uint chunkId = this.HeaderChunkEntries[i].ChunkID;
                try
                {
                    if (ParserFactory.TryGetChunkParser(chunkId, out IChunkParser<Chunk> parser))
                    {
                        headerChunks[i] = parser.Parse(reader, chunkId);
                    }
                    else
                    {
                        headerChunks[i] = new UnknownChunk(reader.ReadRaw((int)this.HeaderChunkEntries[i].ChunkSize), chunkId);
                    }
                }
#if !DEBUG
                catch (Exception ex)
                {
                    ParsingErrorLogger.OnParsingErrorOccured(this, new ParsingErrorEventArgs(chunkId, ex.Message));
                    reader.Stream.Position = start + offset;
                    headerChunks[i] = new UnknownChunk(reader.ReadRaw((int)this.HeaderChunkEntries[i].ChunkSize), chunkId);
                }
#endif
                finally
                {
                    offset += this.HeaderChunkEntries[i].ChunkSize;
                    if (reader.Stream.Position != start + offset)
                    {
                        ParsingErrorLogger.OnParsingErrorOccured(this, new ParsingErrorEventArgs(chunkId, "Invalid stream position after parsing of header chunk."));
                        reader.Stream.Position = start + offset;
                    }
                }
            }
            return headerChunks;
        }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6)]
        public uint NodeCount { get; set; }

#endregion

#region ReferenceTable

        [Property]
        public uint ReferenceTableExternalNodeCount { get; set; }

        [Property, Condition(nameof(ReferenceTableExternalNodeCount), ConditionOperator.GreaterThan, 0)]
        public uint ReferenceTableAncestorLevel { get; set; }

        [Property, Array, Condition(nameof(ReferenceTableExternalNodeCount), ConditionOperator.GreaterThan, 0)]
        public ReferenceTableFolder[] ReferenceTableFolders { get; set; }

        [Property, Array(nameof(ReferenceTableExternalNodeCount)), CustomParserMethod(nameof(ParseReferenceTableExternalNode)), Condition(nameof(ReferenceTableExternalNodeCount), ConditionOperator.GreaterThan, 0)]
        public ReferenceTableExternalNode[] ReferenceTableExternalNodes { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ReferenceTableExternalNode ParseReferenceTableExternalNode(GameBoxReader reader)
        {
            ReferenceTableExternalNode node = new ReferenceTableExternalNode() { FlagsU = reader.ReadUInt32() };
            if (node.Flags.HasFlag(ReferenceTableExternalNodeFlags.Flag3))
            {
                node.FileName = reader.ReadString();
            }
            else
            {
                node.ResourceIndex = reader.ReadUInt32();
            }
            node.NodeIndex = reader.ReadUInt32();
            if (this.Version >= 5)
            {
                node.UseFile = reader.ReadBool();
            }
            if (node.Flags.HasFlag(ReferenceTableExternalNodeFlags.Flag3))
            {
                node.FolderIndex = reader.ReadUInt32();
            }
            return node;
        }

#endregion

#region Body

        [Property, Condition(nameof(BodyCompressed))]
        public uint UncompressedBodySize { get; set; }
        [Property, Condition(nameof(BodyCompressed))]
        public uint CompressedBodySize { get; set; }

        private byte[] rawBodyData;
        private byte[] uncompressedBodyData;
        [Property, CustomParserMethod(nameof(ReadBodyData))]
        public byte[] RawBodyData
        {
            get
            {
                return this.rawBodyData;
            }
            set
            {
                if (value != this.rawBodyData)
                {
                    this.rawBodyData = value;
                    this.uncompressedBodyData = this.BodyCompressed ? null : this.rawBodyData;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] ReadBodyData(GameBoxReader reader)
        {
            Debug.WriteLine("Reading GameBox body data...");
            if (this.BodyCompressed)
            {
                return reader.ReadRaw((int)this.CompressedBodySize);
            }
            else
            {
                return reader.ReadRaw((int)(reader.Stream.Length - reader.Stream.Position));
            }
        }

        public byte[] GetUncompressedBodyData()
        {
            if (this.RawBodyData == null)
            {
                throw new InvalidOperationException("Cannot get uncompressed data when no raw body data has been detected.");
            }
            if (this.uncompressedBodyData != null)
            {
                return this.uncompressedBodyData;
            }
            else
            {
                if (this.UncompressedBodySize > GlobalParserSettings.MaximumUncompressedGameBoxBodySize)
                {
                    throw new InvalidOperationException($"The uncompressed size of the body data exceeds the limit of {GlobalParserSettings.MaximumUncompressedGameBoxBodySize} bytes.");
                }
                this.uncompressedBodyData = new byte[this.UncompressedBodySize];
                MiniLZO.Decompress(this.rawBodyData, this.uncompressedBodyData);
                return this.uncompressedBodyData;
            }
        }

        public IEnumerable<Node> ParseBody()
        {
            HashSet<uint> chunkIds = new HashSet<uint>(ParserFactory.GetParseableIds());
            List<Chunk> chunks = new List<Chunk>();
            using (MemoryStream stream = new MemoryStream(this.GetUncompressedBodyData()))
            using (GameBoxReader reader = new GameBoxReader(stream) { BodyMode = true })
            {
                for (long offset = 0; offset < stream.Length - 4; offset++)
                {
                    stream.Position = offset;
                    uint id = reader.ReadUInt32();
                    if (chunkIds.Contains(id))
                    {
                        try
                        {
                            stream.Position -= 4;
                            if (ParserFactory.TryGetChunkParser(id, out var parser))
                            {
                                Chunk chunk = parser.Parse(reader, id);
                                chunks.Add(chunk);
                            }

                            offset = stream.Position - 1;
                        }
                        catch (Exception ex)
                        {
                            //Do something
                        }
                    }
                }
            }

            return chunks;
        }

#endregion
    }

    [CustomStruct]
    public class HeaderEntry
    {
        [Property]
        public uint ChunkID { get; set; }

        [Property]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public uint ChunkSizeU { get; set; }

        public uint ChunkSize => this.ChunkSizeU & 0x7fffffff;

        public bool IsHeavyChunk => this.ChunkSizeU != this.ChunkSize;
    }

    [CustomStruct]
    public class ReferenceTableFolder
    {
        [Property]
        public string Name { get; set; }

        [Property, Array]
        public ReferenceTableFolder[] SubFolders { get; set; }
    }

    public class ReferenceTableExternalNode
    {
        //The UseFile field is only present if the header version is greater than 5, which is not implementable with auto-generated parsers, hence no parser generation attributes here
        [EditorBrowsable(EditorBrowsableState.Never)]
        public uint FlagsU { get; set; }

        public ReferenceTableExternalNodeFlags Flags => (ReferenceTableExternalNodeFlags)this.FlagsU;

        public string FileName { get; set; }

        public uint ResourceIndex { get; set; }

        public uint NodeIndex { get; set; }

        public bool UseFile { get; set; }

        public uint FolderIndex { get; set; }
    }

    [Flags]
    public enum ReferenceTableExternalNodeFlags
        : uint
    {
        Flag3 = 0b100
    }
}