using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x0304303D, Skippable = true)]
    //The information here is outdated
    public class MapLightmapChunk
        : Chunk
    {
        [Property]
        public bool Unknown { get; set; }

        [Property]
        public uint Version { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 5)]
        public int FrameCount { get; set; } = 1;

        [Property, Array(nameof(FrameCount)), CustomParserMethod(nameof(ParseLightmapFrame))]
        public LightmapFrame[] Frames { get; set; }

        public LightmapFrame ParseLightmapFrame(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            LightmapFrame lightmapFrame = new LightmapFrame();
            lightmapFrame.Image1 = reader.ReadRaw((int)reader.ReadUInt32());
            if (this.Version >= 3) lightmapFrame.Image2 = reader.ReadRaw((int)reader.ReadUInt32());
            if (this.Version >= 6) lightmapFrame.Image3 = reader.ReadRaw((int)reader.ReadUInt32());
            return lightmapFrame;
        }
    }

    public class LightmapFrame
    {
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
    }
}
