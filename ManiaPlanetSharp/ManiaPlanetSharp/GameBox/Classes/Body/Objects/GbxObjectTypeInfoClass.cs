using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectTypeInfoClass
        : GbxClass
    {
        public GbxObjectTypeInfoClass() { }

        [GbxAutoProperty(0)]
        public uint ObjectType { get; set; }
    }
}
