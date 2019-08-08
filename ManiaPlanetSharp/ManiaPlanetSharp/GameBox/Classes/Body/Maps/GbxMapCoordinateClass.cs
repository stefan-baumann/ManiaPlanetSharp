using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxMapCoordinateClass
        : GbxClass
    {
        public GbxVec2D Origin { get; set; }
        public GbxVec2D Target { get; set; }
    }

    public class GbxMapCoordinateClassParser
        : GbxClassParser<GbxMapCoordinateClass>
    {
        protected override int ChunkId => 0x03043025;

        protected override GbxMapCoordinateClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxMapCoordinateClass()
            {
                Origin = reader.ReadVec2D(),
                Target = reader.ReadVec2D()
            };
        }
    }
}
