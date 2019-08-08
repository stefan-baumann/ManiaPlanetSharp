using ManiaPlanetSharp.GameBox.Classes.Ghost;
using ManiaPlanetSharp.GameBox.Classes.Replay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class ReplayMetadataProvider
        : MetadataProvider
    {
        public ReplayMetadataProvider(Stream stream)
            : base(stream)
        { }

        public ReplayMetadataProvider(GameBoxFile file)
            : base(file)
        { }

        //Todo: Implement support for multiple ghosts
        public TimeSpan? RaceTime => this.GetBodyNode<ReplayGhosts>()?.Ghosts.FirstOrDefault()?.OfType<GhostRaceTime>().FirstOrDefault()?.RaceTime;
        public int? RespawnCount => (int?)this.GetBodyNode<ReplayGhosts>()?.Ghosts.FirstOrDefault()?.OfType<GhostRespawnCount>().FirstOrDefault()?.Respawns;
        public int? StuntScore => (int?)this.GetBodyNode<ReplayGhosts>()?.Ghosts.FirstOrDefault()?.OfType<GhostStuntScore>().FirstOrDefault()?.Score;
        public bool? Validable => null;
        public int? CheckpointCount => null;
        public int? CheckpointOneLap => null;
        public string Type => null;

        public string Author => this.GetHeaderNode<ReplayMapAuthor>()?.AuthorLogin;
        public string AuthorNickname => this.GetHeaderNode<ReplayMapAuthor>()?.AuthorNick;
        public string AuthorZone => this.GetHeaderNode<ReplayMapAuthor>()?.AuthorZone;
        public string AuthorExtraInfo => this.GetHeaderNode<ReplayMapAuthor>()?.AuthorExtraInfo;
        public string MapUid => null;
        public string Environment => null;
        public string MapType => null;
        public int? DisplayCost => null;
        public string Mod => null;
        public string Mood => null;
        
        public string Titlepack => null;
        public string Vehicle => null;
        
        public string ExecutableBuildDate => null;
        public string ExecutableVersion => null;

        public byte[] EmbeddedMap => this.GetBodyNode<ReplayEmbeddedMap>()?.Map;
    }

    /*public class GhostMetadata
    {
        public TimeSpan? RaceTime { get; set; }
        public int? RespawnCount { get; set; }
        public int? StuntScore { get; set; }
    }*/
}
