using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0303F005)]
    public class GhostDataChunk
        : Chunk
    {
        [Property]
        public uint UncompressedSize { get; set; }

        /// <summary>
        /// The zlib-deflate-compressed ghost data
        /// </summary>
        [Property, Array]
        public byte[] CompressedData { get; set; }
    }

    [Chunk(0x0303F006)]
    public class GhostDataBoxedChunk
        : Chunk
    {
        [Property]
        public uint IsReplaying { get; set; }

        [Property, CustomParserMethod(nameof(ParseGhostData))]
        public GhostDataChunk GhostData { get; set; }

        public GhostDataChunk ParseGhostData(GameBoxReader reader)
        {
            return ParserFactory.GetChunkParser<GhostDataChunk>().Parse(reader, 0x0303F005);
        }
    }

    [CustomStruct]
    public class GhostData
    {
        [Property]
        public uint ClassId { get; set; }

        [Property]
        public bool SkipList2 { get; set; }

        [Property]
        public uint Unknown1 { get; set; }

        [Property]
        public uint SamplePeriod { get; set; }

        [Property]
        public uint Unknown2 { get; set; }

        [Property, Array]
        public byte[] SampleData { get; set; }

        [Property]
        public uint SampleCount { get; set; }

        [Property, Condition(nameof(SampleCount), ConditionOperator.GreaterThan, 0)]
        public uint FirstSampleOffset { get; set; }

        [Property, Condition(nameof(SampleCount), ConditionOperator.GreaterThan, 1)]
        public uint SizePerSample { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public uint SampleSizeCount => this.SampleCount - 1;

        [Property, Array(nameof(SampleSizeCount)), Condition(nameof(SizePerSample), ConditionOperator.Equal, uint.MaxValue)]
        public uint[] SampleSizes { get; set; }

        [Property, Array, Condition(nameof(SkipList2), ConditionOperator.Equal, false)]
        public int[] SampleTimes { get; set; }
    }

    [CustomStruct]
    public class GhostSampleRecord
    {
        [Property]
        public Vector3D Position { get; set; }

        [Property]
        public ushort AngleU { get; set; }

        public double Angle => this.AngleU * Math.PI / 0xFFFF;

        [Property]
        public short AxisHeadingS { get; set; }

        public double AxisHeading => this.AxisHeadingS * Math.PI / 0x8000;

        [Property]
        public short AxisPitchS { get; set; }

        public double AxisPitch => this.AxisPitchS * (Math.PI / 2) / 0x8000;

        [Property]
        public short SpeedS { get; set; }

        public double Speed => Math.Exp(this.SpeedS / 1000.0);

        [Property]
        public sbyte VelocityHeadingS { get; set; }

        public double VelocityHeading => this.VelocityHeadingS * Math.PI / 0x80;

        [Property]
        public sbyte VelocityPitchS { get; set; }

        public double VelocityPitch => this.VelocityPitchS * (Math.PI / 2) / 0x80;
    }
}
