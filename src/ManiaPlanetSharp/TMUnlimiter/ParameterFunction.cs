namespace ManiaPlanetSharp.TMUnlimiter
{
    public class ParameterFunction
    {
        public string Name { get; private set; }
        public ParameterValueType ValueType { get; private set; }

        public ParameterFunction( string name, ParameterValueType valueType )
        {
            this.Name = name;
            this.ValueType = valueType;
        }
    }
}