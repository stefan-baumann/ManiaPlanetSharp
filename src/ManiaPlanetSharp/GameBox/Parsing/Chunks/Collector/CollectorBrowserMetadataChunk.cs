using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E001009)]
    public class CollectorBrowserMetadataChunk
        : Chunk
    {
        [Property]
        public string PagePath { get; set; }

        [Property]
        public bool HasIconFid { get; set; }

        [Property, Condition(nameof(HasIconFid))]
        public FileReference Icon { get; set; } //Potentially a NodeRef

        [Property(SpecialPropertyType.LookbackString)]
        public string Unused { get; set; }
    }
}
