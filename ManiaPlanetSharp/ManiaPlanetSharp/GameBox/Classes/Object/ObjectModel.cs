using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectModel
        : Node
    {
        public int Version { get; set; }
        public Node PhysicalModel { get; set; }
        public Node VisualModel { get; set; }
        public Node VisualModelStatic { get; set; }

#if HACKY_OBJECT_MODEL_ANALYSIS
        public string MeshName { get; set; }
        public string ShapeName { get; set; }

        public string TriggerShapeName { get; set; }
#endif
    }

    public class ObjectModelParser
        : ClassParser<ObjectModel>
    {
        protected override int ChunkId => 0x2E002019;

        protected override ObjectModel ParseChunkInternal(GameBoxReader reader)
        {
#if !HACKY_OBJECT_MODEL_ANALYSIS
            var result = new ObjectModel()
            {
                Version = reader.ReadInt32(),
                PhysicalModel = reader.ReadNodeReference(),
                VisualModel = reader.ReadNodeReference()
            };
            if (result.Version >= 1)
            {
                result.VisualModelStatic = reader.ReadNodeReference();
            }
#else
            ObjectModel result = new ObjectModel();
            bool meshFound = false, shapeFound = false;
            long start = reader.Stream.Position;
            long end = start;

            for (long offset = 0; (!meshFound || !shapeFound) && (offset < 500) && (reader.Stream.Position < reader.Stream.Length); offset++)
            {
                reader.Stream.Position = start + offset;
                uint u = reader.ReadUInt32();
                if (u == 0x2E006001) //Visual Model/Shape
                {
                    if (this.TryFindString(reader, out string shape, out int distance) && shape.ToLowerInvariant().EndsWith(".shape.gbx"))
                    {
                        shapeFound = true;
                        result.ShapeName = shape;
                        end = reader.Stream.Position;
                        offset = end - start;

                        if (this.TryFindString(reader, out string triggerShape, out _) && triggerShape.ToLowerInvariant().EndsWith(".shape.gbx")) //No easy way to distinguish the item shape and trigger shape, so we'll just rely on their order
                        {
                            result.TriggerShapeName = triggerShape;
                            end = reader.Stream.Position;
                            offset = end - start;
                        }
                    }
                    
                }
                else if (u == 0x2E007001) //Physical Model/Mesh
                {
                    if (this.TryFindString(reader, out string mesh, out _) && mesh.ToLowerInvariant().EndsWith(".mesh.gbx"))
                    {
                        meshFound = true;
                        result.MeshName = mesh;
                        end = reader.Stream.Position;
                        offset = end - start;
                    }
                }
            }
            reader.Stream.Position = end;
#endif

            return result;
        }

#if HACKY_OBJECT_MODEL_ANALYSIS
        private const int maximumStringLength = 50;
        private bool TryFindString(GameBoxReader reader, out string result, out int distance, int maxDistance = 250, int minimumStringLength = 10) //Shortest possible name: A.Mesh.Gbx (10 characters)
        {
            for (long start = reader.Stream.Position, offset = 0; offset < maxDistance && reader.Stream.Position < reader.Stream.Length; offset++)
            {
                reader.Stream.Position = start + offset;
                uint length = reader.ReadUInt32();
                if (length < maximumStringLength && length >= minimumStringLength && reader.Stream.Position + length < reader.Stream.Length) //Potential string
                {
                    bool noControlCharacters = true;
                    for (int i = 0; i < length; i++)
                    {
                        if (char.IsControl((char)reader.ReadByte()))
                        {
                            noControlCharacters = false;
                            break;
                        }
                    }
                    if (noControlCharacters)
                    {
                        reader.Stream.Position = start + offset;
                        result = reader.ReadString();
                        distance = (int)offset;
                        return true;
                    }
                }
            }

            result = null;
            distance = maxDistance;
            return false;
        }
#endif
    }
}
