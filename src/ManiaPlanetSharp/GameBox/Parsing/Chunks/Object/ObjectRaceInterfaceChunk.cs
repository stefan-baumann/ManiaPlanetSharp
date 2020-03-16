using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E00200C)]
    public class ObjectRaceInterfaceChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node RaceInterface { get; set; }
    }
}
