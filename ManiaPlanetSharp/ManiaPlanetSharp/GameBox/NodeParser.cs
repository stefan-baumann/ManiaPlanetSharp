using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class NodeParser
    {
        private const uint EndMarkerClassId = 0xFACADE01;
        private const uint SkipMarker = 0x534B4950; //SKIP
        
        public Node ParseBody(GameBoxReader reader, uint classId)
        {
            //There are multiple separate nodes in the main body
            List<Node> nodes = new List<Node>();
            for (; reader.Stream.Position + 4 < reader.Stream.Length;)
            {
                nodes.Add(this.ParseNode(reader, classId));
            }

            //Assemble them into one single node
            Node body = new Node(classId);
            foreach (Node node in nodes)
            {
                foreach(Node chunk in node)
                {
                    body.Add(chunk);
                }
            }
            return body;
        }

        public Node ParseNode(GameBoxReader reader)
        {
            uint classId = reader.ReadUInt32();
            return this.ParseNode(reader, classId);
        }

        public Node ParseNode(GameBoxReader reader, uint classId)
        {
            Debug.WriteLine($"Starting parsing of node with id 0x{classId:X8}");
            Node node = new Node(classId);
            if (classId == EndMarkerClassId)
            {
                if (this.TrySkipChunk(reader, out Node skipped))
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
                        Node chunk = this.ParseChunk(reader, chunkId);
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

        private Node ParseChunk(GameBoxReader reader, uint chunkId, bool skipIfPossible = false)
        {
            if (chunkId == EndMarkerClassId)
            {
                if (this.TrySkipChunk(reader, out Node skipped))
                {
                    Debug.WriteLine($"  -> Skipped chunk with id 0x{chunkId:X8}");
                    skipped.Class = chunkId;
                    return skipped;
                }
            }
            IClassParser<Node> parser = ClassParser.GetBodyClassParser(chunkId);
            if (parser != null)
            {
                long startPosition = reader.Stream.Position;
                if (parser.Skippable)
                {
                    if (skipIfPossible && this.TrySkipChunk(reader, out Node skipped))
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

#if !DEBUG
                try
                {
#endif
                    Node parsed = parser.ParseChunk(reader);
                    long endPosition = reader.Stream.Position;
                    parsed.Class = chunkId;
                    reader.Stream.Position = startPosition;
                    parsed.Data = reader.ReadRaw((int)(endPosition - startPosition));
                    return parsed;
#if !DEBUG
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[!] Internal Exception of type {ex.GetType()} while parsing chunk {chunkId:X8} with {parser.GetType()}. Terminating parsing.");
                    return null;
                }
#endif
                }
            else if (this.TrySkipChunk(reader, out Node skipped))
            {
                skipped.Class = chunkId;
                return skipped;
            }
            else
            {
                return null;
            }
        }

        private bool TrySkipChunk(GameBoxReader reader, out Node skippedChunk)
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
                skippedChunk = new Node(skip);
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
