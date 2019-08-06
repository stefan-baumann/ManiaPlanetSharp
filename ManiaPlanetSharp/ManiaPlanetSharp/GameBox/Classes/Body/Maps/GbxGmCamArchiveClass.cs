using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxGmCamArchiveClass
        : GbxBodyClass
    {
        public bool HasGmCamArchive { get; set; }
        public byte Unknown1 { get; set; }
        public GbxVec3D[] Unknown2 { get; set; }
        public GbxVec3D Unknown3 { get; set; }
        public float Unknown4 { get; set; }
        public float Unknown5 { get; set; }
        public float Unknown6 { get; set; }
    }

    public class GbxGmCamArchiveClassParser
        : GbxBodyClassParser<GbxGmCamArchiveClass>
    {
        protected override int Chunk => 0x03043027;

        protected override GbxGmCamArchiveClass ParseChunkInternal(GbxReader reader)
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
