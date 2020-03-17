using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectCameraIndex
        : Node
    {
        public uint DefaultCameraIndex { get; set; }
    }

    public class ObjectCameraIndexParser
        : ClassParser<ObjectCameraIndex>
    {
        protected override int ChunkId => 0x2E002006;

        protected override ObjectCameraIndex ParseChunkInternal(GameBoxReader reader)
        {
            return new ObjectCameraIndex()
            {
                DefaultCameraIndex = reader.ReadUInt32()
            };
        }
    }
}
