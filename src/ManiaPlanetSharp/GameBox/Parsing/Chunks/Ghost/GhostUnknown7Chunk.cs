using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092018)]
    public class GhostUnknown7Chunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Unknown1 { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Unknown2 { get; set; }

        [Property(SpecialPropertyType.LookbackString)]
        public string Unknown3 { get; set; }
    }
}
