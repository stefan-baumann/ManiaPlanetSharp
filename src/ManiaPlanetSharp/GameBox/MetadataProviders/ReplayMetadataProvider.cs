using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class ReplayMetadataProvider
        : MetadataProvider
    {
        public ReplayMetadataProvider(GameBoxFile file)
            : base(file)
        { }



        public virtual string MapUid => this.GetBufferedHeaderValue((ReplayVersionChunk c) => c.TrackUid)
            .IfNull((ReplayCommunityChunk c) => c.Root?.Map?.Uid)
            .IgnoreIfEmpty();

        public virtual string MapEnvironment => this.GetBufferedHeaderValue((ReplayVersionChunk c) => c.TrackEnvironment)
            .IgnoreIfEmpty();

        public virtual string MapEnvironmentXml => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Description?.Environment)
            .IgnoreIfEmpty();

        public virtual string MapAuthor => this.GetBufferedHeaderValue((ReplayVersionChunk c) => c.TrackAuthor)
            .IfNull((ReplayCommunityChunk c) => c.Root?.Map?.Author)
            .IgnoreIfEmpty();

        public virtual string MapAuthorZone => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Map?.AuthorZone)
            .IgnoreIfEmpty();

        public virtual string MapAuthorNickname => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Map?.Name)
            .IgnoreIfEmpty();

        public virtual int? MapDisplayCost => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Description?.DisplayCost);

        public virtual string MapStyle => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Description?.MapStyle)
            .IgnoreIfEmpty();

        public virtual string MapType => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Description?.MapType)
            .IgnoreIfEmpty();

        public virtual string MapMod => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Description?.Mod)
            .IgnoreIfEmpty();

        public virtual string MapMood => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Description?.Mood)
            .IgnoreIfEmpty();

        public virtual string Title => this.GetBufferedHeaderValue((ReplayVersionChunk c) => c.TitleUid)
            .IfNull((ReplayCommunityChunk c) => c.Root?.Title)
            .IgnoreIfEmpty();

        public virtual int? MapOneLapCheckpointCount => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Checkpoints?.OneLap);

        public virtual byte[] MapData => this.GetBufferedBodyValue((ReplayMainChunk c) => c.Content is ReplayMainChunkBodyContent content ? content.MapData : null);

        public virtual GameBoxFile MapFile => this.GetBufferedBodyValue((ReplayMainChunk c) => c.Content is ReplayMainChunkBodyContent content ? content.MapFile : null);



        public virtual TimeSpan? ReplayTime => this.GetBufferedHeaderValue((ReplayVersionChunk c) => (TimeSpan?)c.Time)
            .IfNull((ReplayCommunityChunk c) => c.Root?.Times?.Best);

        public virtual string Nickname => this.GetBufferedHeaderValue((ReplayVersionChunk c) => c.Nickname)
            .IfNull((ReplayMainChunk c) => c.Content is ReplayMainChunkHeaderContent content ? content.Nickname : null)
            .IgnoreIfEmpty();

        public virtual string Login => this.GetBufferedHeaderValue((ReplayVersionChunk c) => c.Login)
            .IfNull((ReplayMainChunk c) => c.Content is ReplayMainChunkHeaderContent content ? content.Login : null)
            .IgnoreIfEmpty();

        public virtual string Zone => this.GetBufferedHeaderValue((ReplayMainChunk c) => c.Content is ReplayMainChunkHeaderContent content ? content.Zone : null)
            .IgnoreIfEmpty();

        public virtual string ExtraInfo => this.GetBufferedHeaderValue((ReplayMainChunk c) => c.Content is ReplayMainChunkHeaderContent content ? content.ExtraInfo : null)
            .IgnoreIfEmpty();

        public virtual string Playermodel => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Playermodel?.Id)
            .IgnoreIfEmpty();

        public virtual int? Respawns => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Times?.Respawns);

        public virtual int? StuntScore => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Times?.StuntScore);

        public virtual bool? Validable => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Times?.ValidableBoolean);

        public virtual int? CurrentCheckpointCount => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Checkpoints?.Current);



        public virtual string ExecutableBuildDate => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.ExecutableBuild);

        public virtual string ExecutableVersion => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.ExecutableVersion);

        public virtual string XmlType => this.GetBufferedHeaderValue((ReplayCommunityChunk c) => c.Root?.Type);
    }
}
