using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxBodyClassAutoParser<TBodyClass>
        : GbxBodyClassParser<TBodyClass>
        where TBodyClass : GbxBodyClass, new()
    {
        public GbxBodyClassAutoParser(int chunk)
        {
            this.chunk = chunk;
            this.Fields = typeof(TBodyClass).GetRuntimeProperties()
                .SelectMany(property => property
                    .GetCustomAttributes<GbxAutoPropertyAttribute>()
                    .Select(attribute => new Field(attribute, property)))
                .OrderBy(field => field.Index).ToList();
        }

        private readonly int chunk;
        protected override int Chunk => this.chunk;

        protected List<Field> Fields { get; private set; }

        protected override TBodyClass ParseChunkInternal(GbxReader reader)
        {
            var result = new TBodyClass();
            foreach (Field field in this.Fields)
            {
                this.ParseField(reader, field, result);
            }

            return result;
        }

        protected static Dictionary<Type, Func<GbxReader, object>> supportedTypes = new Dictionary<Type, Func<GbxReader, object>>()
        {
            { typeof(bool), reader => reader.ReadBool() },
            { typeof(byte), reader => reader.ReadByte() },
            { typeof(ushort), reader => reader.ReadUInt16() },
            { typeof(int), reader => reader.ReadInt32() },
            { typeof(uint), reader => reader.ReadUInt32() },
            { typeof(ulong), reader => reader.ReadUInt64() },
            { typeof(ulong[]), reader => reader.ReadUInt128() },
            { typeof(float), reader => reader.ReadFloat() },
            { typeof(string), reader => reader.ReadString() },
            { typeof(GbxFileReference), reader => reader.ReadFileReference() },
            { typeof(GbxNode), reader => reader.ReadNodeReference() },
            { typeof(GbxVec2D), reader => reader.ReadVec2D() },
            { typeof(GbxVec3D), reader => reader.ReadVec3D() }
        };
        protected virtual void ParseField(GbxReader reader, Field field, TBodyClass target)
        {
            field.Property.SetValue(target, field.IsLookbackString ? reader.ReadLookbackString() : supportedTypes[field.Property.PropertyType](reader));
        }

        protected internal class Field
        {
            
            public Field(GbxAutoPropertyAttribute attribute, PropertyInfo property)
            {
                this.Index = attribute.Index;
                if (property.PropertyType == typeof(string))
                {
                    if (attribute is GbxAutoStringPropertyAttribute stringAttribute)
                    {
                        this.IsLookbackString = stringAttribute.IsLookbackString;
                    }
                    else
                    {
                        throw new InvalidOperationException("GbxAutoPropertyAttribute cannot be used on a string property. Use GbxAutoStringPropertyAttribute instead.");
                    }
                }
                else if (!supportedTypes.ContainsKey(property.PropertyType))
                {
                    throw new InvalidOperationException("GbxAutoPropertyAttribute cannot be used on properties of this type.");
                }
                this.Property = property;
            }

            public int Index { get; set; }
            public bool IsLookbackString { get; set; }
            public PropertyInfo Property { get; set; }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class GbxAutoPropertyAttribute
        : Attribute
    {
        public GbxAutoPropertyAttribute(int index)
        {
            this.Index = index;
        }

        public int Index { get; private set; }
    }

    public class GbxAutoStringPropertyAttribute
        : GbxAutoPropertyAttribute
    {
        public GbxAutoStringPropertyAttribute(int index, bool isLoopback)
            : base(index)
        {
            this.IsLookbackString = isLoopback;
        }

        public bool IsLookbackString { get; private set; }
    }
}
