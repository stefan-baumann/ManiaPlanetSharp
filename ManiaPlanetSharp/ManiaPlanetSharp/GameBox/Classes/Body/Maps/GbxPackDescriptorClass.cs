using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxPackDescriptorClass
        : GbxClass
    {
        public string Text { get; set; }
        public GbxFileReference PackDescriptor { get; set; }
        public GbxFileReference ParentPackDescriptor { get; set; }
    }

    public class GbxPackDescriptorClassParser
        : GbxClassParser<GbxPackDescriptorClass>
    {
        protected override int ChunkId => 0x03059002;

        protected override GbxPackDescriptorClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxPackDescriptorClass()
            {
                Text = reader.ReadString(),
                PackDescriptor = reader.ReadFileReference(),
                ParentPackDescriptor = reader.ReadFileReference()
            };
        }
    }
}
