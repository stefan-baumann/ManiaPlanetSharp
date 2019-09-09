using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x2E002013)]
    public class ObjectAudioEnvironmentChunk
        : Chunk
    {
        [Property(SpecialPropertyType.NodeReference)]
        public Node InCarAudioEnvironment { get; set; }
    }
}
