using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Size2D
    {
        public Size2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"Size2D {{ X = {this.X}, Y = {this.Y} }}";
        }
    }
}
