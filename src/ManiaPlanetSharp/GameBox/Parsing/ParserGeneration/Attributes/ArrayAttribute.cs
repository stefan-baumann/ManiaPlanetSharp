using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ArrayAttribute
        : Attribute
    {
        public ArrayAttribute()
        {
            this.LengthSource = new AutomaticArrayLengthSource();
        }

        public ArrayAttribute(int length)
        {
            this.LengthSource = new FixedArrayLengthSource(length);
        }

        public ArrayAttribute(string lengthProperty)
        {
            this.LengthSource = new PropertyArrayLengthSource(lengthProperty);
        }

        public ArrayLengthSource LengthSource { get; private set; }
    }

    public abstract class ArrayLengthSource
    { }

    public class AutomaticArrayLengthSource
        : ArrayLengthSource
    { }

    public class FixedArrayLengthSource
        : ArrayLengthSource
    {
        public FixedArrayLengthSource(int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException();
            this.Length = length;
        }

        public int Length { get; private set; }
    }

    public class PropertyArrayLengthSource
        : ArrayLengthSource
    {
        public PropertyArrayLengthSource(string dependentProperty)
        {
            this.DependentProperty = dependentProperty;
        }

        public string DependentProperty { get; private set; }
    }
}
