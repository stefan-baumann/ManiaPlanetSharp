using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectCameraIndexClass
    : GbxBodyClass
    {
        public uint DefaultCameraIndex { get; set; }
    }

    public class GbxObjectCameraIndexClassParser
        : GbxBodyClassParser<GbxObjectCameraIndexClass>
    {
        protected override int Chunk => 0x2E002006;

        protected override GbxObjectCameraIndexClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxObjectCameraIndexClass()
            {
                DefaultCameraIndex = reader.ReadUInt32()
            };
        }
    }
}
