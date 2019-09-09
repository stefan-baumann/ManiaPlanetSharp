using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    internal static class ParserGenerator
    {
        /* # Todo
         * - Special treatment for properties of type byte[]
         * - Direct call to ParserFactory on property of specified node type
         */

        public static Expression<Func<GameBoxReader, T>> GenerateParserExpression<T>()
            where T : new()
        {
#if !DEBUG
            try
            {
#endif
                ParameterExpression readerParameter = Expression.Parameter(typeof(GameBoxReader), "reader");
                ParameterExpression resultVariable = Expression.Variable(typeof(T), "result");
                List<ParameterExpression> localVariables = new List<ParameterExpression>() { resultVariable };
                IEnumerable<Expression> body =
                    new[] { Expression.Assign(resultVariable, Expression.New(typeof(T))) }
                    .Concat(GenerateFieldParserExpressions<T>(resultVariable, readerParameter, localVariables))
                    .Concat(new[] { Expression.Label(Expression.Label(typeof(T)), resultVariable) } );
                return Expression.Lambda<Func<GameBoxReader, T>>(Expression.Block(localVariables, body), readerParameter);
#if !DEBUG
            }
            catch (Exception ex)
            {
                throw new ParserGeneratorException($"Internal exception occured while generating parser for type {typeof(T).FullName}", ex);
            }
#endif
        }

        private static IEnumerable<Expression> GenerateFieldParserExpressions<T>(Expression target, Expression reader, List<ParameterExpression> localVariables)
        {
            foreach (Field field in GetFields<T>().OrderBy(f => f.Index))
            {
                Expression parsingExpression = Expression.Block(GenerateFieldParserExpression(target, reader, field, localVariables));
                if (field.HasConditions)
                {
                    IEnumerable<Expression> conditionExpressions = GenerateConditionExpressions(target, field);
                    //<condition1> && <condition2> && ...
                    Expression conditionExpression = conditionExpressions.First();
                    foreach(Expression condition in conditionExpressions.Skip(1))
                    {
                        conditionExpression = Expression.AndAlso(conditionExpression, condition);
                    }
                    //if (<conditionExpression>) { <parsingExpression> }
                    yield return Expression.IfThen(conditionExpression, parsingExpression);
                }
                else
                {
                    yield return parsingExpression;
                }
            }
            yield break;
        }

        private static IEnumerable<Expression> GenerateConditionExpressions(Expression target, Field field)
        {
            foreach (Condition condition in field.Conditions)
            {
                Expression dependentProperty = condition is PropertyDependentCondition dependentCondition ? Expression.Property(target, field.Property.DeclaringType.GetProperty(dependentCondition.DependentProperty)) : null;
                switch (condition)
                {
                    case BinaryCondition binaryCondition:
                        if (binaryCondition.ReferenceValue)
                            //yield return Expression.IsTrue(dependentProperty);
                            yield return dependentProperty;
                        else
                            //yield return Expression.IsFalse(dependentProperty);
                            yield return Expression.Negate(dependentProperty);
                        break;
                    case ValueCondition valueCondition:
                        yield return CreateValueConditionExpression(dependentProperty, Expression.Convert(Expression.Constant(valueCondition.ReferenceValue), field.Property.DeclaringType.GetProperty(valueCondition.DependentProperty).PropertyType), valueCondition.Comparison);
                        break;
                    default:
                        throw new NotImplementedException($"Unknown condition at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
            }

            yield break;
        }

        private static IEnumerable<Expression> GenerateFieldParserExpression(Expression target, Expression reader, Field field, List<ParameterExpression> localVariables)
        {
            if (field.HasCustomParser)
            {
                //this.Property = this.CustomParserMethod();
                yield return CreateAssignExpression(target, field, Expression.Call(target, field.Property.DeclaringType.GetMethod(field.CustomParserMethod, BindingFlags.Public | BindingFlags.Instance), reader));
            }
            else
            {
                Expression parseExpression = GenerateSingleValueParseExpression(field, reader);
                if (!field.IsArray)
                {
                    //this.Property = <parseExpression>;
                    yield return CreateAssignExpression(target, field, parseExpression);
                }
                else
                {
                    Expression lengthSource;
                    switch (field.ArrayLengthSource)
                    {
                        case AutomaticArrayLengthSource a:
                            //uint PropertyArrayLength = reader.ReadUInt32();
                            lengthSource = Expression.Variable(typeof(uint), field.Property.Name + "ArrayLength");
                            localVariables.Add((ParameterExpression)lengthSource);
                            yield return Expression.Assign(lengthSource, Expression.Call(reader, nameof(GameBoxReader.ReadUInt32), null));
                            break;
                        case FixedArrayLengthSource fixedLengthSource:
                            lengthSource = Expression.Constant(fixedLengthSource.Length);
                            break;
                        case PropertyArrayLengthSource propertyLengthSource:
                            lengthSource = Expression.Property(target, propertyLengthSource.DependentProperty);
                            break;
                        default:
                            throw new NotImplementedException($"Unknown array length source at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                    }
                    //this.Property = new T[<lengthSource>]
                    yield return CreateAssignExpression(target, field, Expression.NewArrayBounds(field.Property.PropertyType.GetElementType(), Expression.Convert(lengthSource, typeof(int))));
                    //uint iProperty;
                    ParameterExpression index = Expression.Variable(typeof(uint), "i" + field.Property.Name);
                    localVariables.Add(index);
                    //for (iProperty = 0; iProperty < <lengthSource>; iProperty++) { this.Property[iProperty] = <parseExpression>; }
                    yield return CreateForExpression(index, Expression.Constant(0U), lengthSource, CreateArrayAssignExpression(target, field, index, parseExpression));
                }
            }

            yield break;
        }

        private static Dictionary<Type, string> readerMethods = new Dictionary<Type, string>()
        {
            { typeof(bool), nameof(GameBoxReader.ReadBool) },
            { typeof(byte), nameof(GameBoxReader.ReadByte) },
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
        private static Expression GenerateSingleValueParseExpression(Field field, Expression reader)
        {
            Type singleValueType = field.IsArray ? field.Property.PropertyType.GetElementType() : field.Property.PropertyType;
            if (field.HasSpecialPropertyType)
            {
                switch (field.SpecialPropertyType)
                {
                    case SpecialPropertyType.LookbackString:
                        return Expression.Call(reader, nameof(GameBoxReader.ReadLookbackString), null);
                    case SpecialPropertyType.NodeReference:
                        return Expression.Call(reader, nameof(GameBoxReader.ReadNodeReference), null);
                    case SpecialPropertyType.CustomStruct:
                        if (field.Property.PropertyType.GetCustomAttribute<CustomStructAttribute>() != null)
                        {
                            return Expression.Call(typeof(ParserFactory).GetMethod(nameof(ParserFactory.GetCustomStructParser)).MakeGenericMethod(singleValueType));
                        }
                        else
                        {
                            throw new InvalidOperationException($"CustomStruct property has type without CustomStructAttribute at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                        }
                    default:
                        throw new NotImplementedException($"Unknown special property type at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
            }
            else
            {
                if (readerMethods.ContainsKey(singleValueType))
                {
                    return Expression.Call(reader, readerMethods[singleValueType], null);
                }
                else if (typeof(Node).IsAssignableFrom(singleValueType))
                {
                    return Expression.Call(reader, nameof(GameBoxReader.ReadNode), null);
                }
                else
                {
                    throw new NotImplementedException($"Unknown property type at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
            }
        }



        private static Expression CreateAssignExpression(Expression target, Field field, Expression value)
        {
            return Expression.Assign(Expression.Property(target, field.Property), value);
        }

        private static Expression CreateArrayAssignExpression(Expression target, Field field, ParameterExpression index, Expression value)
        {
            //return Expression.Assign(Expression.ArrayIndex(Expression.Property(target, field.Property), Expression.Convert(index, typeof(int))), value);
            return Expression.Assign(Expression.ArrayAccess(Expression.Property(target, field.Property), Expression.Convert(index, typeof(int))), value);
        }

        private static Expression CreateForExpression(ParameterExpression variable, Expression startValue, Expression endValueExclusive, Expression body)
        {
            BinaryExpression startAssigner = Expression.Assign(variable, startValue);
            LabelTarget loopBreakLabel = Expression.Label();
            return Expression.Block(new[] { variable }, startAssigner, Expression.Loop(
                Expression.IfThenElse(Expression.LessThan(variable, endValueExclusive),
                Expression.Block(
                    body,
                    Expression.Increment(variable)
                ),
                Expression.Break(loopBreakLabel)
            ), loopBreakLabel));
        }

        private static Expression CreateValueConditionExpression(Expression left, Expression right, ConditionOperator type)
        {
            switch (type)
            {
                case ConditionOperator.LessThan:
                    return Expression.LessThan(left, right);
                case ConditionOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(left, right);
                case ConditionOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, right);
                case ConditionOperator.Equal:
                    return Expression.Equal(left, right);
                case ConditionOperator.GreaterThan:
                    return Expression.GreaterThan(left, right);
                case ConditionOperator.NotEqual:
                    return Expression.NotEqual(left, right);
                case ConditionOperator.HasFlag:
                    return Expression.Call(left, left.Type.GetMethod("HasFlag"), Expression.Convert(right, typeof(Enum)));
                case ConditionOperator.DoesNotHaveFlag:
                    return Expression.Negate(Expression.Call(left, left.Type.GetMethod("HasFlag"), Expression.Convert(right, typeof(Enum))));
                default:
                    throw new NotImplementedException("Unknown value comparison operation.");
            }
        }



        public static IEnumerable<uint> GetParseableChunkIds<T>()
        {
            foreach (ChunkAttribute chunk in typeof(T).GetCustomAttributes<ChunkAttribute>())
            {
                yield return chunk.Id;
            }
            yield break;
        }

        private static IEnumerable<Field> GetFields<T>()
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                PropertyAttribute propertyAttribute = property.GetCustomAttribute<PropertyAttribute>();
                if (propertyAttribute != null)
                {
                    yield return new Field()
                    {
                        Property = property,
                        Index = propertyAttribute.Line,
                        SpecialPropertyType = propertyAttribute.SpecialType,
                        ArrayLengthSource = property.GetCustomAttribute<ArrayAttribute>()?.LengthSource,
                        Conditions = property.GetCustomAttributes<ConditionAttribute>().Select(conditionAttribute => conditionAttribute.Condition).ToList(),
                        CustomParserMethod = property.GetCustomAttribute<CustomParserMethodAttribute>()?.ParserMethod
                    };
                }
            }
        }

        public class Field
        {
            public PropertyInfo Property { get; set; }
            public int Index { get; set; }

            public bool HasSpecialPropertyType => this.SpecialPropertyType != null;
            public SpecialPropertyType? SpecialPropertyType { get; set; }
            public bool HasConditions => this.Conditions != null ? this.Conditions.Any() : false;
            public List<Condition> Conditions { get; set; }
            public bool IsArray => this.ArrayLengthSource != null;
            public ArrayLengthSource ArrayLengthSource { get; set; }
            public bool HasCustomParser => this.CustomParserMethod != null;
            public string CustomParserMethod { get; set; }
        }
    }
}
