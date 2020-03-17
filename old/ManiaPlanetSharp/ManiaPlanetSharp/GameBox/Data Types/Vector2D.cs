using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Vector2D
    {
        public Vector2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public override string ToString()
        {
            return $"Vector2D {{ X = {this.X}, Y = {this.Y} }}";
        }
    }
}
