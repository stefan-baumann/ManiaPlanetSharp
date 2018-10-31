using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxNodeParser
    {
        public GbxNode ParseNode(GbxReader reader, bool body = false)
        {
            //uint classId = reader.ReadUInt32();
            //Debug.WriteLine($"Starting parsing of node with id {classId:X8}");
            GbxNode chunks = new GbxNode(0);
            //if (classId == 0xFFFFFFFF)
            //{
            //    return chunks;
            //}

            //try
            //{
                for (uint chunkId = reader.ReadUInt32(); reader.Stream.Position + 4 < reader.Stream.Length;)
                {
                    //Debug.WriteLine($"  Found Chunk with id {chunkId:X8}");

                    if (chunkId == 0xFACADE01)
                    {
                        Debug.WriteLine("  -> End of Node");
                        if (body)
                        {
                            Debug.WriteLine("  -> In Main Body, continuing to parse");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        IGbxBodyClassParser<GbxBodyClass> parser = GbxBodyClassParser.GetParser((int)chunkId);
                        if (parser != null)
                        {
                            Debug.WriteLine($"  Found Parser for current chunk ({parser.GetType()})");
                            var result = parser.ParseChunk(reader);
                            chunks.Add(result);
                            Utils.PrintRecursive(result, 1);
                        }
                        else
                        {
                            if (GbxKnownClassIds.IsKnownClassId(chunkId))
                            {
                            Debug.WriteLine($"  Missing Implementation for class id 0x{chunkId:X8} ({GbxKnownClassIds.GetClassName(chunkId)}).");
                            }
                            if (!body)
                            {
                                //throw new Exception();
                            }
                        }
                    }


                    //uint a = reader.ReadUInt32();
                    //if (a == 0x534B4950) //SKIP
                    //{
                    //    reader.Stream.Seek(reader.ReadUInt32(), SeekOrigin.Current);
                    //}
                    //else
                    //{
                    //    chunks.Add(new GbxChunk((int)classId, (int)a, reader.ReadRaw((int)a)));
                    //}

                    if (reader.Stream.Position + 4 <= reader.Stream.Length)
                    {
                        chunkId = reader.ReadUInt32();
                    }
                    else
                    {
                        break;
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"  Caught exception of type {ex.GetType()} with message '{ex.Message}' at {ex.StackTrace}");
            //}

            return chunks;
        }

        public GbxNode ParseSingleNode(GbxReader reader)
        {
            uint chunkId = reader.ReadUInt32();

            if (chunkId == 0xFACADE01)
            {
                Debug.WriteLine("  -> End of Node");
                return new GbxNode(0); //null;
            }
            else
            {
                IGbxBodyClassParser<GbxBodyClass> parser = GbxBodyClassParser.GetParser((int)chunkId);
                if (parser != null)
                {
                    Debug.WriteLine($"  Found Parser for current chunk ({parser.GetType()})");
                    var result = parser.ParseChunk(reader);
                    Utils.PrintRecursive(result, 1);
                    return new GbxNode((int)chunkId) { result };
                }
                else 
                {
                    //throw new Exception();
                    return new GbxNode((int)chunkId);
                }
            }
        }
    }
}
