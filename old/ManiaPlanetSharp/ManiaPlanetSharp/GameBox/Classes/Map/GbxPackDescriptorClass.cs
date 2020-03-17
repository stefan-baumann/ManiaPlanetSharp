using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxPackDescriptorClass
        : Node
    {
        public string Text { get; set; }
        public FileReference PackDescriptor { get; set; }
        public FileReference ParentPackDescriptor { get; set; }
    }

    public class GbxPackDescriptorClassParser
        : ClassParser<GbxPackDescriptorClass>
    {
        protected override int ChunkId => 0x03059002;

        protected override GbxPackDescriptorClass ParseChunkInternal(GameBoxReader reader)
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
