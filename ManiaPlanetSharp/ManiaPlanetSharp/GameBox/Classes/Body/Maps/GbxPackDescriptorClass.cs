using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxPackDescriptorClass
        : GbxBodyClass
    {
        public string Text { get; set; }
        public GbxFileReference PackDescriptor { get; set; }
        public GbxFileReference ParentPackDescriptor { get; set; }
    }

    public class GbxPackDescriptorClassParser
        : GbxBodyClassParser<GbxPackDescriptorClass>
    {
        protected override int Chunk => 0x03059002;

        protected override GbxPackDescriptorClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxPackDescriptorClass()
            {
                Text = reader.ReadString(),
                PackDescriptor = reader.ReadFileRef(),
                ParentPackDescriptor = reader.ReadFileRef()
            };
        }
    }
}
