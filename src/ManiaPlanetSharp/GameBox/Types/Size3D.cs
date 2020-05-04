using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Size3D
        : IEquatable<Size3D>
    {
        public Size3D(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return $"Size3D {{ X = {this.X}, Y = {this.Y}, Z = {this.Z} }}";
        }

        public override bool Equals(object obj)
        {
            return obj is Size3D s && this.Equals(s);
        }

        public bool Equals(Size3D other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        public static bool operator ==(Size3D left, Size3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size3D left, Size3D right)
        {
            return !(left == right);
        }
    }
}
