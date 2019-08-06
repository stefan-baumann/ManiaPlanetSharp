using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxMapCoordinateClass
        : GbxBodyClass
    {
        public GbxVec2D Origin { get; set; }
        public GbxVec2D Target { get; set; }
    }

    public class GbxMapCoordinateClassParser
        : GbxBodyClassParser<GbxMapCoordinateClass>
    {
        protected override int Chunk => 0x03043025;

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
