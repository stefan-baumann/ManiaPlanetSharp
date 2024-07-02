namespace ManiaPlanetSharp.TMUnlimiter
{
    public class Parameter
    {
        public ParameterFunction ParameterFunction { get; private set; }

        public Parameter( ParameterFunction parameterFunction )
        {
            this.ParameterFunction = parameterFunction;
        }
    }

    public class ParameterNumber : Parameter
    {
        public float Value { get; private set; }

        public ParameterNumber( ParameterFunction parameterFunction, float value ) : base( parameterFunction )
        {
            this.Value = value;
        }
    }

    public class ParameterString : Parameter
    {
        public string Value { get; private set; }

        public ParameterString( ParameterFunction parameterFunction, string value ) : base( parameterFunction )
        {
            this.Value = value;
        }
    }
}