using ManiaPlanetSharp.GameBox.Parsing;

namespace ManiaPlanetSharp.TMUnlimiter.Version20
{
    public class VersionBackend : TMUnlimiter.VersionBackend
    {
        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter20;
        }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        public override void ArchiveNew( GameBoxReader reader, uint archiveVersion )
        {
            // No need to implement this chunk (at the moment)
        }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
    }
}