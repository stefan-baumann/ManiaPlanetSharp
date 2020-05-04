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
                
        public static Expression<ChunkParserDelegate<TChunk>> GenerateChunkParserExpression<TChunk>()
            where TChunk : Chunk, new()
        {
#if !DEBUG
            try
            {
#endif
            ParameterExpression readerParameter = Expression.Parameter(typeof(GameBoxReader), "reader");
            ParameterExpression idParameter = Expression.Parameter(typeof(uint), "chunkId");
            ParameterExpression resultVariable = Expression.Variable(typeof(TChunk), "result");
            List<ParameterExpression> localVariables = new List<ParameterExpression>() { resultVariable };
            IEnumerable<Expression> body =
                new[] {
                    Expression.Assign(resultVariable, Expression.New(typeof(TChunk))), //result = new TChunk();
                    Expression.Assign(Expression.Property(resultVariable, nameof(Chunk.Id)), idParameter) //result.Id = chunkId;
                }
                .Concat(GenerateFieldParserExpressions<TChunk>(resultVariable, readerParameter, localVariables))
                .Concat(new[] { Expression.Label(Expression.Label(typeof(TChunk)), resultVariable) });
            return Expression.Lambda<ChunkParserDelegate<TChunk>>(Expression.Block(localVariables, body), readerParameter, idParameter);
#if !DEBUG
            }
            catch (Exception ex)
            {
                throw new ParserGeneratorException($"Internal exception occured while generating parser for type {typeof(TChunk).FullName}", ex);
            }
#endif
        }

        public static Expression<ParserDelegate<TStruct>> GenerateStructParserExpression<TStruct>()
            where TStruct : new()
        {
#if !DEBUG
            try
            {
#endif
            ParameterExpression readerParameter = Expression.Parameter(typeof(GameBoxReader), "reader");
            ParameterExpression resultVariable = Expression.Variable(typeof(TStruct), "result");
            List<ParameterExpression> localVariables = new List<ParameterExpression>() { resultVariable };
            IEnumerable<Expression> body =
                new[] {
                    Expression.Assign(resultVariable, Expression.New(typeof(TStruct))), //result = new TChunk();
                }
                .Concat(GenerateFieldParserExpressions<TStruct>(resultVariable, readerParameter, localVariables))
                .Concat(new[] { Expression.Label(Expression.Label(typeof(TStruct)), resultVariable) });
            return Expression.Lambda<ParserDelegate<TStruct>>(Expression.Block(localVariables, body), readerParameter);
#if !DEBUG
            }
            catch (Exception ex)
            {
                throw new ParserGeneratorException($"Internal exception occured while generating parser for type {typeof(TStruct).FullName}", ex);
            }
#endif
        }



        private static IEnumerable<Expression> GenerateFieldParserExpressions<T>(Expression target, Expression reader, List<ParameterExpression> localVariables)
        {
            foreach (Field field in Fields.GetFields<T>().OrderBy(f => f.Index))
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
            Expression parseExpression;
            if (field.HasCustomParser)
            {
                //result.CustomParserMethod(reader)
                parseExpression = Expression.Call(target, field.Property.DeclaringType.GetMethod(field.CustomParserMethod, BindingFlags.Public | BindingFlags.Instance), reader);
            }
            else
            {
                parseExpression = GenerateSingleValueParseExpression(field, reader);
            }

            if (!field.IsArray)
            {
                //result.Property = <parseExpression>;
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
                        lengthSource = Expression.Convert(Expression.Constant(fixedLengthSource.Length), typeof(uint));
                        break;
                    case PropertyArrayLengthSource propertyLengthSource:
                        lengthSource = Expression.Convert(Expression.Property(target, propertyLengthSource.DependentProperty), typeof(uint));
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

            yield break;
        }

        private static readonly Dictionary<Type, string> readerMethods = new Dictionary<Type, string>()
        {
            { typeof(bool), nameof(GameBoxReader.ReadBool) },
            { typeof(char), nameof(GameBoxReader.ReadChar) },
            { typeof(sbyte), nameof(GameBoxReader.ReadSByte) },
            { typeof(byte), nameof(GameBoxReader.ReadByte) },
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
        private static Expression GenerateSingleValueParseExpression(Field field, Expression reader)
        {
            Type singleValueType = field.IsArray ? field.Property.PropertyType.GetElementType() : field.Property.PropertyType;
            if (field.HasSpecialPropertyType)
            {
                switch (field.SpecialPropertyType)
                {
                    case SpecialPropertyType.LookbackString:
                        if (singleValueType != typeof(string))
                        {
                            throw new InvalidOperationException($"Property marked as lookbackstring is not of type string at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                        }
                        //reader.ReadLookbackString();
                        return Expression.Call(reader, nameof(GameBoxReader.ReadLookbackString), null);
                    case SpecialPropertyType.NodeReference:
                        if (!typeof(Node).IsAssignableFrom(singleValueType))
                        {
                            throw new InvalidOperationException($"Property marked as node reference is not of a type derived of Node at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                        }
                        //reader.ReadNodeReference();
                        return Expression.Call(reader, nameof(GameBoxReader.ReadNodeReference), null);
                    //case SpecialPropertyType.CustomStruct:
                    //    if (singleValueType.GetCustomAttribute<CustomStructAttribute>() != null)
                    //    {
                    //        //ParserFactory.GetCustomStructParser<singleValueType>().Parse(reader)
                    //        return Expression.Call(Expression.Call(typeof(ParserFactory).GetMethod(nameof(ParserFactory.GetCustomStructParser)).MakeGenericMethod(singleValueType)), typeof(CustomStructParser<>).MakeGenericType(singleValueType).GetMethod("Parse"), reader);
                    //    }
                    //    else
                    //    {
                    //        throw new InvalidOperationException($"CustomStruct property has type without CustomStructAttribute at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                    //    }
                    default:
                        throw new NotImplementedException($"Unknown special property type at {field.Property.DeclaringType.Name}.{field.Property.Name}.");
                }
            }
            else
            {
                if (readerMethods.ContainsKey(singleValueType))
                {
                    //reader.Read[...]
                    return Expression.Call(reader, readerMethods[singleValueType], null);
                }
                else if (typeof(Node).IsAssignableFrom(singleValueType))
                {
                    //reader.ReadNode()
                    return Expression.Call(reader, nameof(GameBoxReader.ReadBodyChunk), null);
                }
                else if (singleValueType.GetCustomAttribute<CustomStructAttribute>() != null)
                {
                    //ParserFactory.GetCustomStructParser<singleValueType>().Parse(reader)
                    return Expression.Call(Expression.Call(typeof(ParserFactory).GetMethod(nameof(ParserFactory.GetCustomStructParser)).MakeGenericMethod(singleValueType)), typeof(CustomStructParser<>).MakeGenericType(singleValueType).GetMethod("Parse"), reader);
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



        public static IEnumerable<Tuple<uint, bool>> GetParseableChunkIds<T>()
        {
            foreach (ChunkAttribute chunk in typeof(T).GetCustomAttributes<ChunkAttribute>())
            {
                yield return Tuple.Create(chunk.Id, chunk.Skippable);
            }
            yield break;
        }
    }
}
