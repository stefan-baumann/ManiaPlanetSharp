namespace ManiaPlanetSharp.TMUnlimiter
{
    public class LegacyScript
    {
        public readonly byte[] ByteCode;
        public readonly LegacyScriptExecutionType ExecutionType;

        public LegacyScript( byte[] byteCode, LegacyScriptExecutionType executionType )
        {
            this.ByteCode = byteCode;
            this.ExecutionType = executionType;
        }
    }
}