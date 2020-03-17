using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxMapCoordinateClass
        : Node
    {
        public Vector2D Origin { get; set; }
        public Vector2D Target { get; set; }
    }

    public class GbxMapCoordinateClassParser
        : ClassParser<GbxMapCoordinateClass>
    {
        protected override int ChunkId => 0x03043025;

        protected override GbxMapCoordinateClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxMapCoordinateClass()
            {
                Origin = reader.ReadVec2D(),
                Target = reader.ReadVec2D()
            };
        }
    }
}
