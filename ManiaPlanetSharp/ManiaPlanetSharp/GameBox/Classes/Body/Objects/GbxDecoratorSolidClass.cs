using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxDecoratorSolidClass
        : GbxClass
    {
        public GbxDecoratorSolidClass() { }

        [GbxAutoProperty(0)]
        public GbxNode DecoratorSolid { get; set; }
    }
}
