using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0304300D)]
    public class MapVehicleChunk
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
