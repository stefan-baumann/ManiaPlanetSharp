using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxNodeParser
    {
        private const uint EndMarkerClassId = 0xFACADE01;
        private const uint SkipMarker = 0x534B4950; //SKIP
        
        public GbxNode ParseBody(GbxReader reader, uint classId)
        {
            //There are multiple separate nodes in the main body
            List<GbxNode> nodes = new List<GbxNode>();
            for (; reader.Stream.Position + 4 < reader.Stream.Length;)
            {
                nodes.Add(this.ParseNode(reader, classId));
            }

            //Assemble them into one single node
            GbxNode body = new GbxNode(classId);
            foreach (GbxNode node in nodes)
            {
                foreach(GbxNode chunk in node)
                {
                    body.Add(chunk);
                }
            }
            return body;
        }

        public GbxNode ParseNode(GbxReader reader)
        {
            uint classId = reader.ReadUInt32();
            return this.ParseNode(reader, classId);
        }

        public GbxNode ParseNode(GbxReader reader, uint classId)
        {
            Debug.WriteLine($"Starting parsing of node with id 0x{classId:X8}");
            GbxNode node = new GbxNode(classId);
            if (classId == EndMarkerClassId)
            {
                if (this.TrySkipChunk(reader, out GbxNode skipped))
                {
                    skipped.Class = classId;
                    node.Add(skipped);
                }
            }
            else
            {
                for (; reader.Stream.Position + 4 < reader.Stream.Length;)
                {
                    uint chunkId = reader.ReadUInt32();
                    //Debug.WriteLine($"  Chunk 0x{chunkId:X8}");
                    if (chunkId == EndMarkerClassId)
                    {
                        Debug.WriteLine($"  -> End of Node");
                        break;
                    }
                    else
                    {
                        GbxNode chunk = this.ParseChunk(reader, chunkId);
                        if (chunk != null)
                        {
                            Debug.WriteLine($"  -> Parsed chunk ({chunk.GetType().Name})");
                            
                            node.Add(chunk);
                        }
                        else
                        {
                            //Debug.WriteLine($"  -> Non-parseable chunk");
                            reader.Stream.Position -= 3; //Continue with next byte
                        }
                    }
                }
            }

            return node;
        }

        private GbxNode ParseChunk(GbxReader reader, uint chunkId, bool skipIfPossible = false)
        {
            if (chunkId == EndMarkerClassId)
            {
                if (this.TrySkipChunk(reader, out GbxNode skipped))
                {
                    Debug.WriteLine($"  -> Skipped chunk with id 0x{chunkId:X8}");
                    skipped.Class = chunkId;
                    return skipped;
                }
            }
            IGbxBodyClassParser<GbxBodyClass> parser = GbxBodyClassParser.GetParser(chunkId);
            if (parser != null)
            {
                long startPosition = reader.Stream.Position;
                if (parser.Skippable)
                {
                    if (skipIfPossible && this.TrySkipChunk(reader, out GbxNode skipped))
                    {
                        skipped.Class = chunkId;
                        Debug.WriteLine($"  -> Skipped known skippable chunk with id 0x{chunkId:X8} ({parser.GetType().Name.Replace("Parser", "")})");
                        return skipped;
                    }
                    else
                    {
                        //Read metadata from skippable chunk
                        uint skip = reader.ReadUInt32();
                        uint length = reader.ReadUInt32();
                    }
                }

                try
                {
                    GbxNode parsed = parser.ParseChunk(reader);
                    long endPosition = reader.Stream.Position;
                    parsed.Class = chunkId;
                    reader.Stream.Position = startPosition;
                    parsed.Data = reader.ReadRaw((int)(endPosition - startPosition));
                    return parsed;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[!] Internal Exception of type {ex.GetType()} while parsing chunk {chunkId:X8} with {parser.GetType()}. Terminating parsing.");
                    return null;
                }
            }
            else if (this.TrySkipChunk(reader, out GbxNode skipped))
            {
                skipped.Class = chunkId;
                return skipped;
            }
            else
            {
                return null;
            }
        }

        private bool TrySkipChunk(GbxReader reader, out GbxNode skippedChunk)
        {
            if (reader.Stream.Position + 4 >= reader.Stream.Length)
            {
                skippedChunk = null;
                return false;
            }

            uint skip = reader.ReadUInt32();
            if (skip == SkipMarker)
            {
                int length = (int)reader.ReadUInt32();
                //skippedChunk = new GbxNode((int)skip, length, reader.ReadRaw(length));
                skippedChunk = new GbxNode(skip);
                skippedChunk.Data = reader.ReadRaw(length);
                return true;
            }
            else
            {
                //Go back to position before parsing skip uint
                reader.Stream.Position -= 4; 
                skippedChunk = null;
                return false;
            }
        }
    }
}
