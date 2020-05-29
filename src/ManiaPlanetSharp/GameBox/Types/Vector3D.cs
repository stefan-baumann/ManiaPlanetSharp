using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public struct Vector3D
        : IEquatable<Vector3D>
    {
        public Vector3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"Vector3D {{ X = {this.X}, Y = {this.Y}, Z = {this.Z} }}";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3D v && this.Equals(v);
        }

        public bool Equals(Vector3D other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3D left, Vector3D right)
        {
            return !(left == right);
        }
    }
}
