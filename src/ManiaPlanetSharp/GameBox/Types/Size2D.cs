using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Size2D
        : IEquatable<Size2D>
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

        public override bool Equals(object obj)
        {
            return obj is Size2D s && this.Equals(s);
        }

        public bool Equals(Size2D other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public static bool operator ==(Size2D left, Size2D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size2D left, Size2D right)
        {
            return !(left == right);
        }
    }
}
