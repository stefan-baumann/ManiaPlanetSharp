using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxBody
    {
        public byte[] RawData { get; set; }
        public GbxNode Chunks { get; set; }
    }
}
