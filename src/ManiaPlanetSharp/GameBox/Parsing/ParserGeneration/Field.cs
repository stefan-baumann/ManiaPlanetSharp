using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    public static class Fields
    {
        public static IEnumerable<Field> GetFields<T>()
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
