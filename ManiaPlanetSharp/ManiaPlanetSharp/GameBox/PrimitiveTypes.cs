using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Vec2D
    {
        public Vec2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }
    }
    
    public struct Vec3D
    {
        public Vec3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
    
    public struct Color
    {
        public Color(float r, float g, float b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
    }
}
