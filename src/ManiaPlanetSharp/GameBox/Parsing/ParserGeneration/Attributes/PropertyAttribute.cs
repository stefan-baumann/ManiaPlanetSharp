using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    public enum SpecialPropertyType
    {
        LookbackString,
        NodeReference,
        CustomStruct
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PropertyAttribute
        : Attribute
    {
        public PropertyAttribute([CallerLineNumber]int line = 0)
        {
            this.Line = line;
        }

        public PropertyAttribute(SpecialPropertyType specialType, [CallerLineNumber]int line = 0)
            : this(line)
        {
            this.SpecialType = specialType;
        }

        public SpecialPropertyType? SpecialType { get; private set; }

        protected internal int Line { get; private set; }
    }
}
