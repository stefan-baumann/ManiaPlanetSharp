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

        //public GbxNode ParseNode(GbxReader reader, bool body = false)
        //{
        //    //uint classId = reader.ReadUInt32();
        //    //Debug.WriteLine($"Starting parsing of node with id {classId:X8}");
        //    GbxNode chunks = new GbxNode(0);
        //    //if (classId == 0xFFFFFFFF)
        //    //{
        //    //    return chunks;
        //    //}

        //    //try
        //    //{
        //        for (uint chunkId = reader.ReadUInt32(); reader.Stream.Position + 4 < reader.Stream.Length;)
        //        {
        //            //Debug.WriteLine($"  Found Chunk with id {chunkId:X8}");

        //            if (chunkId == 0xFACADE01)
        //            {
        //                Debug.WriteLine("  -> End of Node");
        //                if (body)
        //                {
        //                    Debug.WriteLine("  -> In Main Body, continuing to parse");
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                IGbxBodyClassParser<GbxBodyClass> parser = GbxBodyClassParser.GetParser((int)chunkId);
        //                if (parser != null)
        //                {
        //                    Debug.WriteLine($"  Found Parser for current chunk ({parser.GetType()})");
        //                    var result = parser.ParseChunk(reader);
        //                    chunks.Add(result);
        //                    Utils.PrintRecursive(result, 1);
        //                }
        //                else
        //                {
        //                    if (GbxKnownClassIds.IsKnownClassId(chunkId))
        //                    {
        //                        Debug.WriteLine($"  Missing Implementation for class id 0x{chunkId:X8} ({GbxKnownClassIds.GetClassName(chunkId)}).");
        //                    }
        //                    if (!body)
        //                    {
        //                        //throw new Exception();
        //                    }
        //                }
        //            }


        //            //uint a = reader.ReadUInt32();
        //            //if (a == 0x534B4950) //SKIP
        //            //{
        //            //    reader.Stream.Seek(reader.ReadUInt32(), SeekOrigin.Current);
        //            //}
        //            //else
        //            //{
        //            //    chunks.Add(new GbxChunk((int)classId, (int)a, reader.ReadRaw((int)a)));
        //            //}

        //            if (reader.Stream.Position + 4 <= reader.Stream.Length)
        //            {
        //                chunkId = reader.ReadUInt32();
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Debug.WriteLine($"  Caught exception of type {ex.GetType()} with message '{ex.Message}' at {ex.StackTrace}");
        //    //}

        //    return chunks;
        //}

        //public GbxNode ParseSingleNode(GbxReader reader)
        //{
        //    uint chunkId = reader.ReadUInt32();

        //    if (chunkId == 0xFACADE01)
        //    {
        //        Debug.WriteLine("  -> End of Node");
        //        return new GbxNode(0); //null;
        //    }
        //    else
        //    {
        //        IGbxBodyClassParser<GbxBodyClass> parser = GbxBodyClassParser.GetParser((int)chunkId);
        //        if (parser != null)
        //        {
        //            Debug.WriteLine($"  Found Parser for current chunk ({parser.GetType()})");
        //            var result = parser.ParseChunk(reader);
        //            Utils.PrintRecursive(result, 1);
        //            return new GbxNode((int)chunkId) { result };
        //        }
        //        else 
        //        {
        //            //throw new Exception();
        //            return new GbxNode((int)chunkId);
        //        }
        //    }
        //}

        public GbxNode ParseBody(GbxReader reader, uint classId)
        {
            //There are multiple separate nodes in the main body
            List<GbxNode> nodes = new List<GbxNode>();
            for (; reader.Stream.Position + 4 < reader.Stream.Length;)
            {
                nodes.Add(this.ParseNode(reader, classId));
            }

            //Assemble them into one single node
            GbxNode body = new GbxNode((int)classId);
            foreach (GbxNode node in nodes)
            {
                foreach(GbxChunk chunk in node)
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
            GbxNode node = new GbxNode((int)classId);
            if (classId == EndMarkerClassId)
            {
                if (this.TrySkipChunk(reader, out GbxChunk skipped))
                {
                    skipped.Id = (int)classId;
                    node.Add(skipped);
                }
            }
            else
            {
                for (; reader.Stream.Position + 4 < reader.Stream.Length;)
                {
                    uint chunkId = reader.ReadUInt32();
                    Debug.WriteLine($"  Chunk 0x{chunkId:X8}");
                    if (chunkId == EndMarkerClassId)
                    {
                        Debug.WriteLine($"  -> End of Node");
                        break;
                    }
                    else
                    {
                        GbxChunk chunk = this.ParseChunk(reader, chunkId);
                        if (chunk != null)
                        {
                            Debug.WriteLine($"  -> Parsed chunk ({chunk.GetType().Name})");
                            //if (!(new[] { typeof(GbxChunk), typeof(GbxUnusedClass) }).Any(t => chunk.GetType() == t))
                            //{
                            //    Utils.PrintRecursive(chunk, 1);
                            //}

                            node.Add(chunk);
                        }
                        else
                        {
                            Debug.WriteLine($"  -> Non-parseable chunk");
                            reader.Stream.Position -= 3; //Continue with next byte
                        }
                    }
                }
            }

            return node;
        }

        private GbxChunk ParseChunk(GbxReader reader, uint chunkId)
        {
            if (chunkId == EndMarkerClassId)
            {
                if (this.TrySkipChunk(reader, out GbxChunk skipped))
                {
                    Debug.WriteLine($"  -> Skipped chunk with id 0x{chunkId:X8}");
                    skipped.Id = (int)chunkId;
                    return skipped;
                }
            }
            IGbxBodyClassParser<GbxBodyClass> parser = GbxBodyClassParser.GetParser((int)chunkId);
            if (parser != null)
            {
                long startPosition = reader.Stream.Position;
                if (parser.Skippable && this.TrySkipChunk(reader, out GbxChunk skipped))
                {
                    skipped.Id = (int)chunkId;
                    Debug.WriteLine($"  -> Skipped known skippable chunk with id 0x{chunkId:X8} ({parser.GetType().Name.Replace("Parser", "")})");
                    return skipped;
                }
                GbxChunk parsed = parser.ParseChunk(reader);
                long endPosition = reader.Stream.Position;
                parsed.Id = (int)chunkId;
                reader.Stream.Position = startPosition;
                parsed.Data = reader.ReadRaw((int)(endPosition - startPosition));
                return parsed;
            }
            else if (this.TrySkipChunk(reader, out GbxChunk skipped))
            {
                skipped.Id = (int)chunkId;
                return skipped;
            }
            else
            {
                return null;
            }
        }

        private bool TrySkipChunk(GbxReader reader, out GbxChunk skippedChunk)
        {
            uint skip = reader.ReadUInt32();
            if (skip == SkipMarker)
            {
                int length = (int)reader.ReadUInt32();
                skippedChunk = new GbxChunk((int)skip, length, reader.ReadRaw(length));
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
