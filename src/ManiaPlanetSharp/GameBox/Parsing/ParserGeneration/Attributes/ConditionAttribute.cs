using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ConditionAttribute
        : Attribute
    {
        protected ConditionAttribute()
        { }

        public ConditionAttribute(string dependentProperty, ConditionOperator conditionOperator, object referenceValue)
            : this()
        {
            this.Condition = new ValueCondition(dependentProperty, conditionOperator, referenceValue);
        }

        public ConditionAttribute(string dependentProperty, bool referenceValue)
            : this()
        {
            this.Condition = new BinaryCondition(dependentProperty, referenceValue);
        }

        public ConditionAttribute(string dependentProperty)
            : this()
        {
            this.Condition = new BinaryCondition(dependentProperty);
        }


        public Condition Condition { get; private set; }
    }

    public abstract class Condition
    { }

    public abstract class PropertyDependentCondition
        : Condition
    {
        protected PropertyDependentCondition(string dependentProperty)
        {
            this.DependentProperty = dependentProperty;
        }

        public string DependentProperty { get; private set; }
    }

    public enum ConditionOperator
    {
        LessThan,
        LessThanOrEqual,
        Equal,
        GreaterThanOrEqual,
        GreaterThan,
        NotEqual,
        HasFlag,
        DoesNotHaveFlag
    }

    public class ValueCondition
        : PropertyDependentCondition
    {
        public ValueCondition(string dependentProperty, ConditionOperator conditionOperator, object referenceValue)
            : base(dependentProperty)
        {
            this.Comparison = conditionOperator;
            this.ReferenceValue = referenceValue;
        }

        public ConditionOperator Comparison { get; private set; }
        public object ReferenceValue { get; private set; }
    }

    public class BinaryCondition
        : PropertyDependentCondition
    {
        public BinaryCondition(string dependentProperty, bool referenceValue)
            : base(dependentProperty)
        {
            this.ReferenceValue = referenceValue;
        }

        public BinaryCondition(string dependentProperty)
            : this(dependentProperty, true)
        { }

        public bool ReferenceValue { get; private set; }
    }
}
