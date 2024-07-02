using ManiaPlanetSharp.GameBox.Parsing;

namespace ManiaPlanetSharp.TMUnlimiter.Vanilla
{
    public class VersionBackend : TMUnlimiter.VersionBackend
    {
        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Vanilla;
        }

        public override void ArchiveNew( GameBoxReader reader, uint archiveVersion )
        {
            // Vanilla track version does not save anything.
        }
    }
}