using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class GbxDecoratorSolidClass
        : GbxClass
    {
        public GbxDecoratorSolidClass() { }

        [GbxAutoProperty(0)]
        public GbxNode DecoratorSolid { get; set; }
    }
}
