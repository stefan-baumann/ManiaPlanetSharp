namespace ManiaPlanetSharp.TMUnlimiter
{
    public class LegacyMediaClipResource
    {
        public uint MediaClipIndex { get; private set; }
        public object Resource { get; private set; }

        public LegacyMediaClipResource( uint mediaClipIndex, object resource )
        {
            this.MediaClipIndex = mediaClipIndex;
            this.Resource = resource;
        }

        public ParameterSet GetParameterSet()
        {
            return
            (
                this.Resource != null && this.Resource is ParameterSet parameterSet
                ?
                parameterSet
                :
                null
            );
        }

        public LegacyScript GetLegacyScript()
        {
            return
            (
                this.Resource != null && this.Resource is LegacyScript legacyScript
                ?
                legacyScript
                :
                null
            );
        }
    }
}