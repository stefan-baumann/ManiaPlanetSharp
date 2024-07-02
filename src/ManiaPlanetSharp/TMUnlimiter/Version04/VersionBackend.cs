namespace ManiaPlanetSharp.TMUnlimiter.Version04
{
    public class VersionBackend : TMUnlimiter.VersionBackend
    {
        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter04;
        }
    }
}