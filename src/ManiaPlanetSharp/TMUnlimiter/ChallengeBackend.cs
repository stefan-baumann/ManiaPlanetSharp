using ManiaPlanetSharp.GameBox.Parsing;
using System;

namespace ManiaPlanetSharp.TMUnlimiter
{
    public class ChallengeBackend
    {
        public VersionBackend VersionBackend { get; set; }
        public bool IsLegacyFormat { get; set; }

        public ChallengeBackend( TrackVersion trackVersion )
        {
            switch ( trackVersion )
            {
                case TrackVersion.Vanilla:
                {
                    this.VersionBackend = new Vanilla.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter04:
                {
                    this.VersionBackend = new Version04.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter06:
                {
                    this.VersionBackend = new Version06.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter07:
                {
                    this.VersionBackend = new Version07.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter11:
                {
                    this.VersionBackend = new Version11.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter12:
                {
                    this.VersionBackend = new Version12.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter13:
                {
                    this.VersionBackend = new Version13.VersionBackend();
                    break;
                }
                case TrackVersion.Unlimiter20:
                {
                    this.VersionBackend = new Version20.VersionBackend();
                    break;
                }
                default:
                {
                    throw new NotSupportedException( $"Unsupported track version = { trackVersion }" );
                }
            }
        }

        public TrackVersion GetTrackVersion()
        {
            return this.VersionBackend.GetTrackVersion();
        }

        public void ArchiveOld( GameBoxReader reader )
        {
            this.IsLegacyFormat = true;
            this.VersionBackend.ArchiveOld( reader );
        }

        public void ArchiveNew( GameBoxReader reader, uint archiveVersion )
        {
            this.IsLegacyFormat = false;
            this.VersionBackend.ArchiveNew( reader, archiveVersion );
        }
    }
}