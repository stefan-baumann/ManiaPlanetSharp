using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class GbxObjectCameraIndexClass
    : GbxClass
    {
        public uint DefaultCameraIndex { get; set; }
    }

    public class GbxObjectCameraIndexClassParser
        : GbxClassParser<GbxObjectCameraIndexClass>
    {
        protected override int ChunkId => 0x2E002006;

        protected override GbxObjectCameraIndexClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxObjectCameraIndexClass()
            {
                DefaultCameraIndex = reader.ReadUInt32()
            };
        }
    }
}
