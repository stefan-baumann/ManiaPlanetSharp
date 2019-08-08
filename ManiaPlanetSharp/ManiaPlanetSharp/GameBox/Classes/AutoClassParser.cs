using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace ManiaPlanetSharp.GameBox
{
    public class AutoClassParser<TBodyClass>
        : ClassParser<TBodyClass>
        where TBodyClass : Node, new()
    {
        public AutoClassParser(int chunk)
        {
            this.chunk = chunk;
            this.Fields = typeof(TBodyClass).GetRuntimeProperties()
                .SelectMany(property => property
                    .GetCustomAttributes<AutoParserPropertyAttribute>()
                    .Select(attribute => new Field(attribute, property)))
                .OrderBy(field => field.Index).ToList();
        }

        private readonly int chunk;
        protected override int ChunkId => this.chunk;

        protected List<Field> Fields { get; private set; }

        protected override TBodyClass ParseChunkInternal(GameBoxReader reader)
        {
            var result = new TBodyClass();
            foreach (Field field in this.Fields)
            {
                this.ParseField(reader, field, result);
            }

            return result;
        }

        protected static Dictionary<Type, Func<GameBoxReader, object>> supportedTypes = new Dictionary<Type, Func<GameBoxReader, object>>()
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
            { typeof(FileReference), reader => reader.ReadFileReference() },
            { typeof(Node), reader => reader.ReadNodeReference() },
            { typeof(Vector2D), reader => reader.ReadVec2D() },
            { typeof(Vector3D), reader => reader.ReadVec3D() }
        };
        protected virtual void ParseField(GameBoxReader reader, Field field, TBodyClass target)
        {
            field.Property.SetValue(target, field.IsLookbackString ? reader.ReadLookbackString() : supportedTypes[field.Property.PropertyType](reader));
        }

        protected internal class Field
        {
            
            public Field(AutoParserPropertyAttribute attribute, PropertyInfo property)
            {
                this.Index = attribute.Index;
                if (property.PropertyType == typeof(string))
                {
                    if (attribute is AutoParserStringPropertyAttribute stringAttribute)
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
    public class AutoParserPropertyAttribute
        : Attribute
    {
        public AutoParserPropertyAttribute(int index)
        {
            this.Index = index;
        }

        public int Index { get; private set; }
    }

    public class AutoParserStringPropertyAttribute
        : AutoParserPropertyAttribute
    {
        public AutoParserStringPropertyAttribute(int index, bool isLoopback)
            : base(index)
        {
            this.IsLookbackString = isLoopback;
        }

        public bool IsLookbackString { get; private set; }
    }
}
