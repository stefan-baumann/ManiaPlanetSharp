using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using ManiaPlanetSharp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class MapMetadataProvider
        : MetadataProvider
    {
        public MapMetadataProvider(GameBoxFile file)
            : base(file)
        { }



        public virtual string Name => this.GetBufferedHeaderValue((MapCommonChunk c) => c.Name)
            .IfNull((MapCommunityChunk c) => c.Root?.Identity?.Name)
            .IfNullBody((MapChunk c) => c.Name);

        public virtual string Environment => this.GetBufferedHeaderValue((MapCommonChunk c) => c.Environment)
            .IfNull((MapCommunityChunk c) => c.Root?.Description?.Environment)
            .IfNullBody((MapChunk c) => c.Environment);

        public virtual string Author => this.GetBufferedHeaderValue((MapCommonChunk c) => c.Author)
            .IfNull((MapAuthorChunk c) => c.Login)
            .IfNullBody((MapChunk c) => c.Author);

        public virtual string AuthorNickname => this.GetBufferedHeaderValue((MapAuthorChunk c) => c.Nick);

        public virtual string AuthorZone => this.GetBufferedHeaderValue((MapAuthorChunk c) => c.Zone)
            .IfNull((MapCommunityChunk c) => c.Root?.Identity?.AuthorZone);

        public virtual string Uid => this.GetBufferedHeaderValue((MapCommonChunk c) => c.Uid)
            .IfNull((MapCommunityChunk c) => c.Root?.Identity?.Uid)
            .IfNullBody((MapChunk c) => c.Uid);

        public virtual string Titlepack => this.GetBufferedHeaderValue((MapCommonChunk c) => c.TitleUid)
            .IfNull((MapCommunityChunk c) => c.Root?.Title);

        public virtual int? Checkpoints => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (int?)c.Checkpoints);

        public virtual bool? UsesAdvancedEditor => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (bool?)c.AdvancedEditor);

        public virtual bool? HasGhostBlocks => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (bool?)c.HasGhostBlocks)
            .IfNull((MapCommunityChunk c) => c.Root?.Description?.HasGhostBlocks);

        public virtual bool? IsMultilap => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (bool?)c.Multilap);

        public virtual int? Laps => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (int?)c.Laps)
            .IfNull((MapCommunityChunk c) => c.Root?.Description?.LapCount.Modify(i => i == 0 ? 1 : i));

        public virtual int? DisplayCost => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (int?)c.Cost)
            .IfNull((MapCommunityChunk c) => c.Root?.Description?.DisplayCost);

        public virtual bool? Validated => this.GetBufferedHeaderValue((MapCommunityChunk c) => c.Root?.Description?.Validated);

        public virtual string TypeFull => this.GetBufferedHeaderValue((MapCommonChunk c) => c.Type);

        public virtual string Type => this.GetBufferedHeaderValue((MapDescriptionChunk c) => c.TrackType.ToString());

        public virtual MapKind? Kind => this.GetBufferedHeaderValue((MapCommonChunk c) => (MapKind?)c.Kind);

        public virtual bool? Locked => this.GetBufferedHeaderValue((MapCommonChunk c) => (bool?)c.Locked)
            .IfNullBody((MapChunk c) => c.NeedsUnlock);

        public virtual string Mod => this.GetBufferedHeaderValue((MapCommunityChunk c) => c.Root?.Description.Mod);



        public virtual byte[] Thumbnail => this.GetBufferedHeaderValue((MapThumbnailChunk c) => c.ThumbnailData);

        public virtual string Comment => this.GetBufferedHeaderValue((MapThumbnailChunk c) => c.Comment);



        public virtual string Vehicle => this.GetBufferedHeaderValue((MapCommunityChunk c) => c.Root?.PlayerModel?.Id)
            .IfNullBody((MapVehicleChunk c) => c.Name);

        public virtual string VehicleAuthor => this.GetBufferedBodyValue((MapVehicleChunk c) => c.Author);

        public virtual string VehicleCollection => this.GetBufferedBodyValue((MapVehicleChunk c) => c.Collection);



        public virtual TimeSpan? BronzeTime => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (TimeSpan?)c.BronzeTimeSpan)
            .IfNull((MapCommunityChunk c) => c.Root?.Times?.BronzeTimeSpan);

        public virtual TimeSpan? SilverTime => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (TimeSpan?)c.SilverTimeSpan)
            .IfNull((MapCommunityChunk c) => c.Root?.Times?.SilverTimeSpan);

        public virtual TimeSpan? GoldTime => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (TimeSpan?)c.GoldTimeSpan)
            .IfNull((MapCommunityChunk c) => c.Root?.Times?.GoldTimeSpan);

        public virtual TimeSpan? AuthorTime => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (TimeSpan?)c.AuthorTimeSpan)
            .IfNull((MapCommunityChunk c) => c.Root?.Times?.AuthorTimeSpan)
            .IfNullBody((MapTimelimitChunk c) => c.AuthorTime);

        public virtual int? AuthorScore => this.GetBufferedHeaderValue((MapDescriptionChunk c) => (int?)c.AuthorScore)
            .IfNull((MapCommunityChunk c) => c.Root?.Times?.AuthorScore);



        public virtual string TimeOfDay => this.GetBufferedHeaderValue((MapCommonChunk c) => c.DecorationTimeOfDay)
            .IfNullBody((MapChunk c) => c.TimeOfDay);

        public virtual string DecorationEnvironment => this.GetBufferedHeaderValue((MapCommonChunk c) => c.DecorationEnvironment)
            .IfNullBody((MapChunk c) => c.DecorationEnvironment);

        public virtual string DecorationEnvironmentAuthor => this.GetBufferedHeaderValue((MapCommonChunk c) => c.DecorationEnvironmentAuthor)
            .IfNullBody((MapChunk c) => c.DecorationEnvironmentAuthor);

        public virtual Size3D? Size => this.GetBufferedBodyValue((MapChunk c) => (Size3D?)c.Size);

        public virtual Vector2D? Origin => this.GetBufferedHeaderValue((MapCommonChunk c) => (Vector2D?)c.Origin)
            .IfNullBody((MapCoordinateChunk c) => (Vector2D?)c.Origin);

        public virtual Vector2D? Target => this.GetBufferedHeaderValue((MapCommonChunk c) => (Vector2D?)c.Target)
            .IfNullBody((MapCoordinateChunk c) => (Vector2D?)c.Target);

        public virtual int? LightmapVersion => this.GetBufferedHeaderValue((MapCommonChunk c) => (int?)c.LightmapVersion)
            .IfNull((MapCommunityChunk c) => c.Root?.Lightmap);
        public virtual FileReference CustomMusic => this.GetBufferedBodyValue((MapCustomMusicChunk c) => c.CustomMusic);

        public virtual int? EmbeddedItemSize => this.GetBufferedBodyValue((MapEmbeddedItemsChunk c) => (int?)c.ZipSize);

        //public virtual int? EmbeddedItemInGameSize => this.GetBufferedBodyValue((MapEmbeddedItemsChunk c) => (int?)c.EmbeddedItemSize);

        public virtual EmbeddedItem[] EmbeddedItems => this.GetBufferedBodyValue((MapEmbeddedItemsChunk c) => c.Items);

        public virtual Dependency[] Dependencies => this.GetBufferedHeaderValue((MapCommunityChunk c) => c.Root?.Dependencies.Deps.ToArray());



        public virtual Block[] Blocks => this.GetBufferedBodyValue((MapChunk c) => c.Blocks);



        public virtual string ExecutableBuildDate => this.GetBufferedHeaderValue((MapCommunityChunk c) => c.Root?.ExecutableBuildDate);

        public virtual string ExecutableVersion => this.GetBufferedHeaderValue((MapCommunityChunk c) => c.Root?.ExecutableVersion);



        public EmbeddedItemFile[] GetEmbeddedItemFiles()
        {
            return this.GetBodyNodes<MapEmbeddedItemsChunk>()?.FirstOrDefault()?.GetEmbeddedItemFiles().ToArray();
        }
    }
}
