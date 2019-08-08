using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxLightmapClass
        : GbxBodyClass
    {
        public bool Unknown { get; set; }
        public int Version { get; set; }
        public int FrameCount { get; set; }
        public GbxLightmapFrame[] Frames { get; set; }
    }

    public class GbxLightmapFrame
    {
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
    }

    public class GbxLightmapClassParser
        : GbxBodyClassParser<GbxLightmapClass>
    {
        protected override int Chunk => 0x0304303D;

        protected override GbxLightmapClass ParseChunkInternal(GbxReader reader)
        {
            //Something might be wrong here - check MT-Wiki for details
            GbxLightmapClass result = new GbxLightmapClass();
            result.Unknown = reader.ReadBool();
            result.Version = (int)reader.ReadUInt32();
            result.FrameCount = result.Version >= 5 ? (int)reader.ReadUInt32() : 1;
            if (result.Version >= 2)
            {
                result.Frames = new GbxLightmapFrame[result.FrameCount];
                for (int i = 0; i < result.FrameCount; i++)
                {
                    GbxLightmapFrame frame = new GbxLightmapFrame();
                    frame.Image1 = reader.ReadRaw((int)reader.ReadUInt32());
                    if (result.Version >= 3) frame.Image2 = reader.ReadRaw((int)reader.ReadUInt32());
                    if (result.Version >= 6) frame.Image3 = reader.ReadRaw((int)reader.ReadUInt32());
                    result.Frames[i] = frame;
                }
            }

            return result;
        }
    }
}
