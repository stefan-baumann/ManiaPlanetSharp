using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Vector2D
        : IEquatable<Vector2D>
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

        public override bool Equals(object obj)
        {
            return obj is Vector2D v && this.Equals(v);
        }

        public bool Equals(Vector2D other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public static bool operator ==(Vector2D left, Vector2D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2D left, Vector2D right)
        {
            return !(left == right);
        }
    }
}
