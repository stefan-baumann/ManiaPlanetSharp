using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x2E00100B)]
    public class CollectorBasicMetadataChunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Name { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Collection { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Author { get; set; }
    }
}
