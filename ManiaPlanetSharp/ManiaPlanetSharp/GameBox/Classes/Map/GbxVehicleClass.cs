using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxVehicleClass
        : Node
    {
        public string Name { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
    }

    public class GbxVehicleClassParser
        : ClassParser<GbxVehicleClass>
    {
        protected override int ChunkId => 0x0304300D;

        protected override GbxVehicleClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxVehicleClass()
            {
                Name = reader.ReadLookbackString(),
                Collection = reader.ReadLookbackString(),
                Author = reader.ReadLookbackString()
            };
        }
    }
}
