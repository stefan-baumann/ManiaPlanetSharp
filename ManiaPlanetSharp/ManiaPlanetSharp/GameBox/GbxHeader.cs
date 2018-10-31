using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxHeader
    {
        public GbxHeader()
        { }

        public ushort Version { get; set; }
        public GbxFormat Format { get; set; }
        public bool CompressedReferenceTable { get; set; }
        public bool CompressedBody { get; set; }
        public uint MainClassID { get; set; }
        public uint UserDataSize { get; set; }
        public byte[] UserData { get; set; }
        public GbxNode Chunks { get; set; }
        public uint NodeCount { get; set; }
    }
}
