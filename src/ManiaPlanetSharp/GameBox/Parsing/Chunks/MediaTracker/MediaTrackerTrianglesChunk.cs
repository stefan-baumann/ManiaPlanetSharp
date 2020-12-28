using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks.MediaTracker
{
    [Chunk(0x03029001)]
    [Chunk(0x03029002)]
    public class MediaTrackerTrianglesChunk
    : Chunk
    {
        [Property, Array]
        public MediaTrackerTrianglesValue[] Values { get; set; }

        [Property]
        public uint TimestepCount { get; set; }

        [Property]
        public uint VerticeCount { get; set; }

        [Property, CustomParserMethod(nameof(MediaTrackerTrianglesChunk.ParseVertices))]
        public Vector3D[][] VerticeLocations { get; set; }

        public Vector3D[][] ParseVertices(GameBoxReader reader)
        {
            List<Vector3D[]> timesteps = new List<Vector3D[]>();
            for (int i = 0; i < this.TimestepCount; i++)
            {
                List<Vector3D> buffer = new List<Vector3D>();
                for (int j = 0; j < this.VerticeCount; j++)
                {
                    buffer.Add(reader.ReadVec3D());
                }
                timesteps.Add(buffer.ToArray());
            }
            return timesteps.ToArray();
        }

        [Property, Array]
        public MediaTrackerTrianglesColor[] VerticeColors { get; set; }

        [Property, Array]
        public MediaTrackerTrianglesTriangleVerticeIndices[] Triangles { get; set; }

        [Property]
        public uint Unknown1 { get; set; }

        [Property]
        public uint Unknown2 { get; set; }

        [Property]
        public uint Unknown3 { get; set; }

        [Property]
        public float Unknown4 { get; set; }

        [Property]
        public uint Unknown5 { get; set; }

        [Property]
        public uint Unknown6 { get; set; }

        [Property]
        public uint Unknown7 { get; set; }
    }

    [CustomStruct]
    public class MediaTrackerTrianglesValue
    {
        [Property]
        public float Timestamp { get; set; }
    }

    [CustomStruct]
    public class MediaTrackerTrianglesColor
    {
        [Property]
        public float R { get; set; }

        [Property]
        public float G { get; set; }

        [Property]
        public float B { get; set; }

        [Property]
        public float A { get; set; }
    }

    [CustomStruct]
    public class MediaTrackerTrianglesTriangleVerticeIndices
    {
        [Property, Array(3)]
        public int[] VerticeIndices { get; set; }
    }
}
