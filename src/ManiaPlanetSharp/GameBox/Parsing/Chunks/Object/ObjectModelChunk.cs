#define HACKY_OBJECT_MODEL_DETECTION

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E002019)]
    public class ObjectModelChunk
        : Chunk
    {
        [Property]
        public int Version { get; set; }

        ////if ((itemType != Ornament && itemType != Spot && itemType != PickUp) || (version < 9)) && (itemType != Vehicle || (version < 10)) && (version < 12):
        //[Property(SpecialPropertyType.NodeReference)]
        //public Node PhysicalModel { get; set; }

        ////Same condition as PhysicalModel
        //[Property(SpecialPropertyType.NodeReference)]
        //public Node VisualModel { get; set; }

        //[Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.Equal, 1)]
        //public Node VisualModelStatic { get; set; }

        //[Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 3)]
        //public int Unknown1 { get; set; }

        //[Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 4)]
        //public Node Unknown2 { get; set; }

        //[Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 5)]
        //public Node Unknown3 { get; set; }

        //[Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 6)]
        //public int UnknownCount { get; set; }

        //[Property, Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 7)]
        //public int Unknown4 { get; set; }

        //[Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 8)]
        //public Node Unknown5 { get; set; }

        //[Property(SpecialPropertyType.NodeReference), Condition(nameof(ObjectModelChunk.Version), ConditionOperator.GreaterThanOrEqual, 8), Condition(nameof(ObjectModelChunk.Unknown5), ConditionOperator.Equal, null)]
        //public Node Unknown6 { get; set; }

#if HACKY_OBJECT_MODEL_DETECTION
        //For IX, we need reliable detection of the linked shape, triggershape and mesh files in items. As the knowledge we have about this chunk and its subchunks does not suffice to reliably get that information in some cases, we resort to rather "primitive" methods of finding those file paths, implemented here

        [Property, CustomParserMethod(nameof(FindShapeName))]
        public string ShapeName { get; set; }

        [Property, CustomParserMethod(nameof(FindTriggerShapeName))]
        public string TriggerShapeName { get; set; }

        [Property, CustomParserMethod(nameof(FindMeshName))]
        public string MeshName { get; set; }



        public string FindShapeName(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            long start = reader.Stream.Position;
            if (this.TryAdvanceUntilChunkStart(reader, 0x2E006001) && this.TryFindString(reader, name => name.EndsWith(".shape.gbx", StringComparison.InvariantCultureIgnoreCase), out string shapeName))
            {
                return shapeName;
            }
            reader.Stream.Position = start;
            return null;
        }

        public string FindTriggerShapeName(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            long start = reader.Stream.Position;
            if (this.ShapeName != null && this.TryFindString(reader, name => name.EndsWith(".shape.gbx", StringComparison.InvariantCultureIgnoreCase), out string triggerShapeName))
            {
                return triggerShapeName;
            }
            reader.Stream.Position = start;
            return null;
        }

        public string FindMeshName(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            long start = reader.Stream.Position;
            if (this.TryAdvanceUntilChunkStart(reader, 0x2E007001) && this.TryFindString(reader, name => name.EndsWith(".mesh.gbx", StringComparison.InvariantCultureIgnoreCase), out string meshName))
            {
                return meshName;
            }
            reader.Stream.Position = start;
            return null;
        }

        private bool TryAdvanceUntilChunkStart(GameBoxReader reader, uint chunkId, int maxDistance = 1000)
        {
            long start = reader.Stream.Position;
            for (long offset = 0; offset < maxDistance && reader.Stream.Position < reader.Stream.Length; offset++)
            {
                reader.Stream.Position = start + offset;
                if (reader.ReadUInt32() == chunkId)
                {
                    return true;
                }
            }
            reader.Stream.Position = start;
            return false;
        }

        private const int maximumStringLength = 250;
        private bool TryFindString(GameBoxReader reader, Func<string, bool> condition, out string result, int maxDistance = 500, int minimumStringLength = 10) //Shortest possible name: A.Mesh.Gbx (10 characters)
        {
            long start = reader.Stream.Position;
            for (long offset = 0; offset < maxDistance && reader.Stream.Position < reader.Stream.Length; offset++)
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
                        if (condition(result))
                        {
                            return true;
                        }
                    }
                }
            }

            result = null;
            reader.Stream.Position = start;
            return false;
        }

        #endif
    }
}
