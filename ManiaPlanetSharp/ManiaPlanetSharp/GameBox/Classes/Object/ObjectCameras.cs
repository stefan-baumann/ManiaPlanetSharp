using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectCameras
        : Node
    {
        public uint Version { get; set; }
        public uint CameraCount { get; set; }
        public Node[] Cameras { get; set; }
    }

    public class ObjectCamerassParser
        : ClassParser<ObjectCameras>
    {
        protected override int ChunkId => 0x2E002009;

        protected override ObjectCameras ParseChunkInternal(GameBoxReader reader)
        {
            var result = new ObjectCameras();
            result.Version = reader.ReadUInt32();
            result.CameraCount = reader.ReadUInt32();
            result.Cameras = new Node[result.CameraCount];
            for (int i = 0; i < result.CameraCount; i++)
            {
                result.Cameras[i] = reader.ReadNodeReference();
            }
            return result;
        }
    }
}
