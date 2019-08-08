using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectCameraClass
    : GbxClass
    {
        public uint Version { get; set; }
        public uint CameraCount { get; set; }
        public GbxNode[] Cameras { get; set; }
    }

    public class GbxObjectCameraClassParser
        : GbxClassParser<GbxObjectCameraClass>
    {
        protected override int ChunkId => 0x2E002009;

        protected override GbxObjectCameraClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxObjectCameraClass();
            result.Version = reader.ReadUInt32();
            result.CameraCount = reader.ReadUInt32();
            result.Cameras = new GbxNode[result.CameraCount];
            for (int i = 0; i < result.CameraCount; i++)
            {
                result.Cameras[i] = reader.ReadNodeReference();
            }
            return result;
        }
    }
}
