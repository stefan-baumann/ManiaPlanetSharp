using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxWaypointSpecialPropertyClass
        : GbxBodyClass
    {
        public uint Version { get; set; }
        public uint Spawn { get; set; }
        public uint Order { get; set; }
        public string Tag { get; set; }
    }

    public class GbxWaypointSpecialPropertyClassParser
        : GbxBodyClassParser<GbxWaypointSpecialPropertyClass>
    {
        protected override int Chunk => 0x2E009000;

        protected override GbxWaypointSpecialPropertyClass ParseChunkInternal(GbxReader reader)
        {
            GbxWaypointSpecialPropertyClass waypoint = new GbxWaypointSpecialPropertyClass();
            waypoint.Version = reader.ReadUInt32();
            //if (waypoint.Version == this.Chunk) waypoint.Version = reader.ReadUInt32();
            switch (waypoint.Version)
            {
                case 1:
                    waypoint.Spawn = reader.ReadUInt32();
                    waypoint.Order = reader.ReadUInt32();
                    break;
                case 2:
                    waypoint.Tag = reader.ReadString();
                    waypoint.Order = reader.ReadUInt32();
                    break;
                default: //Test Fix
                    //waypoint.Tag = reader.ReadLoopbackString();
                    //reader.ReadRaw(5);
                    //waypoint.Order = reader.ReadUInt32();
                    break;
            }

            return waypoint;
        }
    }
}
