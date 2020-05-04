using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    public static class ParserCodeGenerator
    {
        public static string GenerateChunkParserString<TChunk>()
            where TChunk : Chunk, new()
        {
#if !DEBUG
            try
            {
#endif
            IndentingStringBuilder builder = new IndentingStringBuilder();
            builder.AppendLine($"var result = new {typeof(TChunk).FullName}() {{ Id = chunkId }};");

            GenerateFieldsParseCode<TChunk>(builder);

            builder.AppendLine($"return result;");
            return builder.ToString();
#if !DEBUG
            }
            catch (Exception ex)
            {
                throw new ParserGeneratorException($"Internal exception occured while generating parser for type {typeof(TChunk).FullName}", ex);
            }
#endif
        }
        public static string GenerateStructParserString<TStruct>()
            where TStruct : new()
        {
#if !DEBUG
            try
            {
#endif
            IndentingStringBuilder builder = new IndentingStringBuilder();
            builder.AppendLine($"var result = new {typeof(TStruct).FullName}();");

            GenerateFieldsParseCode<TStruct>(builder);

            builder.AppendLine($"return result;");
            return builder.ToString();
#if !DEBUG
            }
            catch (Exception ex)
            {
                throw new ParserGeneratorException($"Internal exception occured while generating parser for type {typeof(TStruct).FullName}", ex);
            }
#endif
        }

        public static List<Tuple<uint, bool>> GetParserChunkIds<TChunk>()
            where TChunk : Chunk, new()
        {
            return typeof(TChunk).GetCustomAttributes<ChunkAttribute>().Select(c => Tuple.Create(c.Id, c.Skippable)).ToList();
        }

        private static void GenerateFieldsParseCode<T>(IndentingStringBuilder builder)
        {
            foreach (Field field in Fields.GetFields<T>().OrderBy(f => f.Index))
            {
                if (field.HasConditions)
                {
                    builder.AppendLine($"if ({string.Join(" && ", field.Conditions.Select(condition => GenerateConditionCode(field, condition)))})");
                    builder.AppendLine("{");
                    builder.Indent();
                    GenerateFieldParseCode(field, builder);
                    builder.UnIndent();
                    builder.AppendLine("}");
                }
                else
                {
                    GenerateFieldParseCode(field, builder);
                }
            }
        }

        private static void GenerateFieldParseCode(Field field, IndentingStringBuilder builder)
        {
            string parseCode;
            Type singleValueType = field.IsArray ? field.Property.PropertyType.GetElementType() : field.Property.PropertyType;
            if (singleValueType == null)
            {
                throw new ArgumentNullException($"The single value type of property {field.Property.Name} is null. This could be caused by adding an [Array] attribute to a property whose type is not an array.");
            }

            if (field.HasCustomParser)
            {
                parseCode = $"result.{field.CustomParserMethod}(reader)";
            }
            else if (field.HasSpecialPropertyType)
            {
                switch (field.SpecialPropertyType)
                {
                    case SpecialPropertyType.LookbackString:
                        if (singleValueType != typeof(string))
                        {
                            throw new InvalidOperationException($"Property marked as lookbackstring is not of type string at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                        }
                        parseCode = "reader.ReadLookbackString()";
                        break;
                    case SpecialPropertyType.NodeReference:
                        if (!typeof(Node).IsAssignableFrom(singleValueType))
                        {
                            throw new InvalidOperationException($"Property marked as node reference is not of a type derived of Node at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                        }
                        parseCode = singleValueType == typeof(Node) ? "reader.ReadNodeReference()" : $"({singleValueType.FullName})reader.ReadNodeReference()";
                        break;
                    default:
                        throw new NotImplementedException($"Unknown special property type at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
            }
            else
            {
                if (readerMethods.ContainsKey(singleValueType))
                {
                    parseCode = $"reader.{readerMethods[singleValueType]}()";
                }
                else if (typeof(Node).IsAssignableFrom(singleValueType))
                {
                    parseCode = singleValueType == typeof(Node) ? "reader.ReadNode()" : $"({singleValueType.FullName})reader.ReadNode()";
                }
                else if (singleValueType.GetCustomAttribute<CustomStructAttribute>() != null)
                {
                    parseCode = $"ParserFactory.GetCustomStructParser<{singleValueType.FullName}>().Parse(reader)";
                }
                else
                {
                    throw new NotImplementedException($"Unknown property type at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
            }

            if (!field.IsArray)
            {
                builder.AppendLine($"result.{field.Property.Name} = {parseCode};");
            }
            else
            {
                string lengthSource;
                switch (field.ArrayLengthSource)
                {
                    case AutomaticArrayLengthSource _:
                        lengthSource = "reader.ReadUInt32()";
                        break;
                    case FixedArrayLengthSource fixedLengthSource:
                        lengthSource = fixedLengthSource.Length.ToString(CultureInfo.InvariantCulture);
                        break;
                    case PropertyArrayLengthSource propertyLengthSource:
                        lengthSource = $"(uint)result.{propertyLengthSource.DependentProperty}";
                        break;
                    default:
                        throw new NotImplementedException($"Unknown array length source at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
                if (singleValueType == typeof(byte))
                {
                    builder.AppendLine($"result.{field.Property.Name} = reader.ReadRaw((int){lengthSource});");
                }
                else
                {
                    builder.AppendLine($"result.{field.Property.Name} = new {field.Property.PropertyType.GetElementType()}[{lengthSource}];");
                    builder.AppendLine($"for (int i = 0; i < result.{field.Property.Name}.Length; i++)");
                    builder.AppendLine("{");
                    builder.Indent();
                    builder.AppendLine($"result.{field.Property.Name}[i] = {parseCode};");
                    builder.UnIndent();
                    builder.AppendLine("}");
                }
            }
        }

        private static readonly Dictionary<Type, string> readerMethods = new Dictionary<Type, string>()
        {
            { typeof(bool), nameof(GameBoxReader.ReadBool) },
            { typeof(byte), nameof(GameBoxReader.ReadByte) },
            { typeof(sbyte), nameof(GameBoxReader.ReadSByte) },
            { typeof(char), nameof(GameBoxReader.ReadChar) },
            { typeof(short), nameof(GameBoxReader.ReadInt16) },
            { typeof(ushort), nameof(GameBoxReader.ReadUInt16) },
            { typeof(int), nameof(GameBoxReader.ReadInt32) },
            { typeof(uint), nameof(GameBoxReader.ReadUInt32) },
            { typeof(ulong), nameof(GameBoxReader.ReadUInt64) },
            //{ typeof(uint128), nameof(GameBoxReader) },
            { typeof(float), nameof(GameBoxReader.ReadFloat) },
            { typeof(string), nameof(GameBoxReader.ReadString) },
            { typeof(FileReference), nameof(GameBoxReader.ReadFileReference) },
            { typeof(Vector2D), nameof(GameBoxReader.ReadVec2D) },
            { typeof(Vector3D), nameof(GameBoxReader.ReadVec3D) },
            { typeof(Size2D), nameof(GameBoxReader.ReadSize2D) },
            { typeof(Size3D), nameof(GameBoxReader.ReadSize3D) },
        };

        private static string GenerateConditionCode(Field field, Condition condition)
        {
            switch (condition)
            {
                case BinaryCondition binaryCondition:
                    return "result." + (binaryCondition.ReferenceValue == true ? binaryCondition.DependentProperty : "!" + binaryCondition.DependentProperty);
                case ValueCondition valueCondition:
                    string valueCode = valueCondition.ReferenceValue == null ? "null" : valueCondition.ReferenceValue is string s ? $"\"{s}\"" : valueCondition.ReferenceValue is Enum e ? $"{e.GetType().FullName}.{Enum.GetName(e.GetType(), e)}" : valueCondition.ReferenceValue is bool b ? (b ? "true" : "false") : valueCondition.ReferenceValue.ToString();
                    
                    switch (valueCondition.Comparison)
                    {
                        case ConditionOperator.LessThan:
                            return $"result.{valueCondition.DependentProperty} < {valueCode}";
                        case ConditionOperator.LessThanOrEqual:
                            return $"result.{valueCondition.DependentProperty} <= {valueCode}";
                        case ConditionOperator.GreaterThanOrEqual:
                            return $"result.{valueCondition.DependentProperty} >= {valueCode}";
                        case ConditionOperator.Equal:
                            return $"result.{valueCondition.DependentProperty} == {valueCode}";
                        case ConditionOperator.GreaterThan:
                            return $"result.{valueCondition.DependentProperty} > {valueCode}";
                        case ConditionOperator.NotEqual:
                            return $"result.{valueCondition.DependentProperty} != {valueCode}";
                        case ConditionOperator.HasFlag:
                            return $"result.{valueCondition.DependentProperty}.HasFlag({valueCode})";
                        case ConditionOperator.DoesNotHaveFlag:
                            return $"!result.{valueCondition.DependentProperty}.HasFlag({valueCode})";
                        default:
                            throw new NotImplementedException("Unknown value comparison operation.");
                    }
                default:
                    throw new NotImplementedException($"Unknown condition at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
            }
        }
    }
}
