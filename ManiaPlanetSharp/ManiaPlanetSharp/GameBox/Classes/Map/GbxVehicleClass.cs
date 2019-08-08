using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxVehicleClass
        : Node
    {
        public uint Version { get; set; }
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
                Version = reader.ReadUInt32(),
                Name = reader.ReadLookbackString(),
                Collection = reader.ReadLookbackString(),
                Author = reader.ReadLookbackString()
            };
        }
    }
}
