using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxGmCamArchiveClass
        : Node
    {
        public bool HasGmCamArchive { get; set; }
        public byte Unknown1 { get; set; }
        public Vector3D[] Unknown2 { get; set; }
        public Vector3D Unknown3 { get; set; }
        public float Unknown4 { get; set; }
        public float Unknown5 { get; set; }
        public float Unknown6 { get; set; }
    }

    public class GbxGmCamArchiveClassParser
        : ClassParser<GbxGmCamArchiveClass>
    {
        protected override int ChunkId => 0x03043027;

        protected override GbxGmCamArchiveClass ParseChunkInternal(GameBoxReader reader)
        {
            if (reader.ReadBool())
            {
                return new GbxGmCamArchiveClass()
                {
                    Unknown1 = reader.ReadByte(),
                    Unknown2 = new[] { reader.ReadVec3D(), reader.ReadVec3D(), reader.ReadVec3D() },
                    Unknown3 = reader.ReadVec3D(),
                    Unknown4 = reader.ReadFloat(),
                    Unknown5 = reader.ReadFloat(),
                    Unknown6 = reader.ReadFloat()
                };
            }
            else
            {
                return new GbxGmCamArchiveClass()
                {
                    HasGmCamArchive = false
                };
            }
        }
    }
}
