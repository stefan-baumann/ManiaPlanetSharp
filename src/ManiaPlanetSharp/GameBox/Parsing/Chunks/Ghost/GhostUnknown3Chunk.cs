using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092010)]
    public class GhostUnknown3Chunk
        : Chunk
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string Unknown { get; set; }
    }
}
