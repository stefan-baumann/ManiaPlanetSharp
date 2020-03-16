using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03043027)]
    public class MapGmCamArchiveChunk
        : Chunk
    {
        [Property]
        public bool HasGmCamArchive { get; set; }

        [Property, Condition(nameof(HasGmCamArchive))]
        public byte Unknown1 { get; set; }

        [Property, Array(3), Condition(nameof(HasGmCamArchive))]
        public Vector3D[] Unknown2 { get; set; }

        [Property, Condition(nameof(HasGmCamArchive))]
        public Vector3D Unknown3 { get; set; }

        [Property, Condition(nameof(HasGmCamArchive))]
        public float Unknown4 { get; set; }

        [Property, Condition(nameof(HasGmCamArchive))]
        public float Unknown5 { get; set; }

        [Property, Condition(nameof(HasGmCamArchive))]
        public float Unknown6 { get; set; }
    }
}
