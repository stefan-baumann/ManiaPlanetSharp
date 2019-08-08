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
        public AutoClassParser(int chunk, bool skippable = false)
        {
            this.chunk = chunk;
            this.skippable = skippable;
            this.Fields = typeof(TBodyClass).GetRuntimeProperties()
                .SelectMany(property => property
                    .GetCustomAttributes<AutoParserPropertyAttribute>()
                    .Select(attribute => new Field(attribute, property)))
                .OrderBy(field => field.Index).ToList();
        }

        private readonly int chunk;
        protected override int ChunkId => this.chunk;

        private readonly bool skippable;
        public override bool Skippable => this.skippable;

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
            if (field.IsLookbackString)
            {
                field.Property.SetValue(target, reader.ReadLookbackString());
            }
            else if (field.IsArray)
            {
                var countField = this.Fields.FirstOrDefault(f => f.Index == field.ArrayCountIndex);
                if (countField == null)
                {
                    throw new InvalidOperationException("Referenced field for auto parser array does not exist.");
                }
                else
                {
                    var count = (int)countField.Property.GetValue(target);
                    var array = Array.CreateInstance(field.ArrayType, count);
                    for (int i = 0; i < count; i++)
                    {
                        array.SetValue(supportedTypes[field.ArrayType](reader), i);
                    }
                    field.Property.SetValue(target, array);
                }
            }
            else
            {
                field.Property.SetValue(target, supportedTypes[field.Property.PropertyType](reader));
            }
            
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
                        throw new InvalidOperationException($"AutoParserPropertyAttribute cannot be used on a string property. Use AutoParserStringPropertyAttribute instead. Property is {property.DeclaringType.FullName}.{property.Name}.");
                    }
                }
                else if (!supportedTypes.ContainsKey(property.PropertyType))
                {
                    if (attribute is AutoParserArrayPropertyAttribute arrayAttribute)
                    {
                        if (!property.PropertyType.IsArray || !supportedTypes.ContainsKey(property.PropertyType.GetElementType()))
                        {
                            throw new InvalidOperationException($"AutoParserArrayPropertyAttribute cannot be used on properties of this type. Property is {property.DeclaringType.FullName}.{property.Name}.");
                        }
                        this.IsArray = true;
                        this.ArrayCountIndex = arrayAttribute.CountIndex;
                        this.ArrayType = property.PropertyType.GetElementType();
                    }
                    else
                    {
                        throw new InvalidOperationException($"AutoParserPropertyAttribute cannot be used on properties of this type. Property is {property.DeclaringType.FullName}.{property.Name}.");
                    }
                }
                this.Property = property;
            }

            public int Index { get; set; }
            public bool IsLookbackString { get; set; }
            public bool IsArray { get; set; }
            public int ArrayCountIndex { get; set; } = -1;
            public Type ArrayType { get; set; }
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

    public class AutoParserArrayPropertyAttribute
        : AutoParserPropertyAttribute
    {
        public AutoParserArrayPropertyAttribute(int index, int countIndex)
            : base(index)
        {
            if (index <= countIndex)
            {
                throw new InvalidOperationException("The count index must refer to an element that is parsed before the array.");
            }
            this.CountIndex = countIndex;
        }

        public int CountIndex { get; private set; }
    }
}
