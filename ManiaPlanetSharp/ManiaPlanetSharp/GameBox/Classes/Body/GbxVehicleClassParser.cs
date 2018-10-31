using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxVehicleClass
        : GbxBodyClass
    {
        public uint Version { get; set; }
        public string Name { get; set; }
        public string Collection { get; set; }
        public string Author { get; set; }
    }

    public class GbxVehicleClassParser
        : GbxBodyClassParser<GbxVehicleClass>
    {
        protected override int Chunk => 0x0304300D;

        protected override GbxVehicleClass ParseChunkInternal(GbxReader reader)
        {
            GbxVehicleClass vehicle = new GbxVehicleClass();
            vehicle.Version = reader.ReadUInt32();
            vehicle.Name = reader.ReadLoopbackString();
            vehicle.Collection = reader.ReadLoopbackString();
            vehicle.Author = reader.ReadLoopbackString();

            return vehicle;
        }
    }
}
