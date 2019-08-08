using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class FileHeader
        : Node
    {
        public FileHeader()
        { }

        public ushort Version { get; set; }
        public FileFormat Format { get; set; }
        public bool CompressedReferenceTable { get; set; }
        public bool CompressedBody { get; set; }
        public uint MainClassID { get; set; }
        public uint UserDataSize { get; set; }
        public byte[] UserData { get; set; }
        public uint NodeCount { get; set; }
    }
}
