using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorBrowserMetadataClass
        : GbxClass
    {
        public GbxCollectorBrowserMetadataClass()
        { }

        public string PagePath { get; set; }
        public bool HasIconFid { get; set; }
        public GbxFileReference Icon { get; set; } //Potentially a NodeRef
        public string Unused { get; set; }
    }

    public class GbxCollectorBrowserMetadataClassParser
        : GbxClassParser<GbxCollectorBrowserMetadataClass>
    {
        protected override int ChunkId => 0x2E001009;

        protected override GbxCollectorBrowserMetadataClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxCollectorBrowserMetadataClass()
            {
                PagePath = reader.ReadString(),
                HasIconFid = reader.ReadBool()
            };
            if (result.HasIconFid)
            {
                result.Icon = reader.ReadFileReference();
            }
            result.Unused = reader.ReadLookbackString();

            return result;
        }
    }
}
