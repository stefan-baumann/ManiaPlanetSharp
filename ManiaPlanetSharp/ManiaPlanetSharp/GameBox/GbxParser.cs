using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using ManiaPlanetSharp.Utilities;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GbxParser"/> class.
        /// </summary>
        /// <param name="stream">The gbx file stream.</param>
        public GbxParser(Stream stream)
        {
            this.Reader = new GbxReader(stream);
        }

        /// <summary>
        /// Returns the reader used for reading the gbx file.
        /// </summary>
        /// <value>
        /// The reader used for reading the gbx file.
        /// </value>
        protected internal GbxReader Reader { get; private set; }

        public virtual Tuple<GbxHeader, GbxReferenceTable, GbxChallengeClass[], GbxBody> Parse()
        {
            GbxHeader header = this.ParseHeader(this.Reader);
            GbxReferenceTable referenceTable = this.ParseReferenceTable(this.Reader);
            GbxChallengeClass[] chunks = header.Chunks.Select(chunk => (GbxChallengeClassParser.GetParser(chunk.Class))?.ParseChunk(chunk)).Where(data => data != null).ToArray();
            GbxBody body = this.ParseBody(this.Reader, header);
            return Tuple.Create(header, referenceTable, chunks, body);
        }

        #region Header

        protected GbxHeader ParseHeader(GbxReader reader)
        {
            //Check for magic string
            if (!this.ReadMagicString(reader)) throw new Exception("Magic string is invalid.");

            GbxHeader header = new GbxHeader();
            header.Version = reader.ReadUInt16();

            //Read general metrics
            if (header.Version >= 3)
            {
                header.Format = (char)reader.ReadByte() == 'T' ? GbxFormat.Text : GbxFormat.Binary;
                header.CompressedReferenceTable = (char)reader.ReadByte() == 'C';
                header.CompressedBody = (char)reader.ReadByte() == 'C';

                if (header.Version >= 4)
                {
                    char unused = (char)reader.ReadByte();
                }

                header.MainClassID = reader.ReadUInt32();

                //Parse chunks
                if (header.Version >= 6)
                {
                    header.UserDataSize = reader.ReadUInt32();
                    header.UserData = reader.ReadRaw((int)header.UserDataSize);
                    header.Chunks = this.ParseChunks(header.UserData);
                }

                header.NodeCount = reader.ReadUInt32();
            }

            return header;
        }

        protected bool ReadMagicString(GbxReader reader)
        {
            return reader.ReadString(3) == "GBX";
        }
        
        protected GbxNode ParseChunks(byte[] userData)
        {
            //Parse chunk metadata
            using (MemoryStream userDataStream = new MemoryStream(userData))
            using (GbxReader reader = new GbxReader(userDataStream))
            {
                uint chunkCount = reader.ReadUInt32();
                Dictionary<uint, int> chunkMetadata = new Dictionary<uint, int>();
                for (int i = 0; i < chunkCount; i++)
                {
                    chunkMetadata.Add(reader.ReadUInt32(), (int)(reader.ReadUInt32() & 0x7fffffff));
                }

                //Parse chunks sorted by id (because they are layed out that way in the file)
                GbxNode chunks = new GbxNode(67);
                foreach (var chunk in chunkMetadata.OrderBy(c => c.Key))
                {
                    GbxNode chunkNode = new GbxNode(chunk.Key);
                    chunkNode.Data = reader.ReadRaw(chunk.Value);
                    chunks.Add(chunkNode);
                }
                return chunks;
            }
        }

        #endregion

        #region ReferenceTable

        protected GbxReferenceTable ParseReferenceTable(GbxReader reader)
        {
            GbxReferenceTable referenceTable = new GbxReferenceTable();
            referenceTable.ExternalNodeCount = reader.ReadUInt32();
            if (referenceTable.ExternalNodeCount > 0)
            {
                referenceTable.AncestorLevel = reader.ReadUInt32();
                referenceTable.SubFolderCount = reader.ReadUInt32();
                for (int i = 0; i < referenceTable.SubFolderCount; i++)
                {
                    referenceTable.SubFolders.Add(this.ParseFolder(reader));
                }
                for (int i = 0; i < referenceTable.ExternalNodeCount; i++)
                {
                    referenceTable.ExternalNodes.Add(this.ParseExternalNode(reader));
                }
            }
            return referenceTable;
        }

        protected GbxReferenceTableFolder ParseFolder(GbxReader reader)
        {
            GbxReferenceTableFolder folder = new GbxReferenceTableFolder();
            folder.SubFolderCount = reader.ReadUInt32();
            for (int i = 0; i < folder.SubFolderCount; i++)
            {
                folder.SubFolders.Add(this.ParseFolder(reader));
            }

            return folder;
        }

        protected GbxReferenceTableExternalNode ParseExternalNode(GbxReader reader)
        {
            GbxReferenceTableExternalNode externalNode = new GbxReferenceTableExternalNode();
            externalNode.Flags = reader.ReadUInt32();
            if (!externalNode.HasFlag(3))
            {
                externalNode.FileName = reader.ReadString();
            }
            else
            {
                externalNode.ResourceIndex = reader.ReadUInt32();
            }
            externalNode.NodeIndex = reader.ReadUInt32();
            //if (headerVersion >= 5)
            externalNode.UseFile = reader.ReadBool();
            if (!externalNode.HasFlag(3))
            {
                externalNode.FolderIndex = reader.ReadUInt32();
            }

            return externalNode;
        }

        #endregion

        #region Body

        protected GbxBody ParseBody(GbxReader reader, GbxHeader header)
        {
            GbxBody body = new GbxBody();
            body.RawData = this.GetUncompressedData(reader, header.CompressedBody);

            using (MemoryStream stream = new MemoryStream(body.RawData))
            using (GbxReader bodyReader = new GbxReader(stream))
            {
                body.Chunks = new GbxNodeParser().ParseBody(bodyReader, header.MainClassID);
            }

            return body;
        }

        protected byte[] GetUncompressedData(GbxReader reader, bool bodyCompressed)
        {
            if (!bodyCompressed)
            {
                return reader.ReadRaw((int)(reader.Stream.Length - reader.Stream.Position));
            }
            else
            {
                uint uncompressedSize = reader.ReadUInt32();
                uint compressedSize = reader.ReadUInt32();
                if (uncompressedSize > 50000000) throw new ArgumentOutOfRangeException("The uncompressed data exceeds the limit of 50MB!");
                byte[] compressed = reader.ReadRaw((int)compressedSize);
                byte[] uncompressed = new byte[uncompressedSize];
                MiniLZO.Decompress(compressed, uncompressed);
                return uncompressed;
            }
        }



        #endregion
    }
}
