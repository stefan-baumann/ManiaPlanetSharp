using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x0301B000)]
    public class CollectorListChunk
        : Chunk
    {        
        [Property, Array]
        public CollectorStock[] Archive { get; set; }
    }

    [CustomStruct]
    public class CollectorStock
    {
        [Property(SpecialPropertyType.LookbackString)]
        public string BlockName { get; set; }
        [Property(SpecialPropertyType.LookbackString)]

        public string Collection { get; set; }
        [Property(SpecialPropertyType.LookbackString)]

        public string Author { get; set; }

        [Property]
        public uint Data { get; set; }
    }
}
