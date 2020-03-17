using ManiaPlanetSharp.GameBox.Classes.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class MapMetadataProvider
        : MetadataProvider
    {
        public MapMetadataProvider(Stream stream)
            : base(stream)
        { }

        public MapMetadataProvider(GameBoxFile file)
            : base(file)
        { }

        //Basic Metadata
        public string Name => this.GetBodyNode<GbxMapClass>()?.MapName ?? this.GetHeaderNode<GbxCommonClass>()?.MapName;
        public string Environment => this.GetBodyNode<GbxMapClass>()?.Environment ?? this.GetHeaderNode<GbxCommonClass>()?.MapEnvironment;
        public string Author => this.GetBodyNode<GbxMapClass>()?.Author ?? this.GetHeaderNode<GbxCommonClass>()?.MapAuthor;
        public string AuthorNickname => this.GetHeaderNode<GbxAuthorClass>()?.Nick ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Identity?.Name;
        public string AuthorZone => this.GetHeaderNode<GbxAuthorClass>()?.Zone ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Identity.AuthorZone;
        public string AuthorExtraInfo => this.GetHeaderNode<GbxAuthorClass>()?.ExtraInfo;
        public string Uid => this.GetBodyNode<GbxMapClass>()?.Uid ?? this.GetHeaderNode<GbxCommonClass>()?.MapUid ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Identity.Uid;
        public string Titlepack => this.GetHeaderNode<GbxCommonClass>().TitleUid ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Title;
        public int? Checkpoints => (int?)this.GetHeaderNode<GbxTmDescriptionClass>()?.Checkpoints;
        public bool? AdvancedEditor => this.GetHeaderNode<GbxTmDescriptionClass>()?.AdvancedEditor;
        public bool? HasGhostBlocks => this.GetHeaderNode<GbxTmDescriptionClass>()?.HasGhostBlocks ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Description?.HasGhostBlocks;
        public bool? Multilap => this.GetHeaderNode<GbxTmDescriptionClass>()?.Multilap;
        public int? Laps => (int?)this.GetHeaderNode<GbxTmDescriptionClass>()?.Laps ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Description?.LapCount;
        public int? DisplayCost => (int?)this.GetHeaderNode<GbxTmDescriptionClass>()?.Cost ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Description?.DisplayCost;
        public bool? Validated => this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Description?.Validated;
        public string Type => this.GetHeaderNode<GbxCommonClass>()?.MapType ?? this.GetHeaderNode<GbxTmDescriptionClass>()?.TrackType.ToString();
        public GbxMapKind? Kind => this.GetHeaderNode<GbxCommonClass>()?.Kind;
        public bool? Locked => this.GetHeaderNode<GbxCommonClass>()?.Locked ?? this.GetBodyNode<GbxMapClass>()?.NeedsUnlock;
        public string Mod => this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Description?.Mod;

        public byte[] Thumbnail => this.GetHeaderNode<GbxThumbnailClass>()?.ThumbnailData;
        public string Comment => this.GetHeaderNode<GbxThumbnailClass>()?.Comment;

        public string Vehicle => this.GetBodyNode<GbxVehicleClass>()?.Name ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.PlayerModel?.Id;
        public string VehicleAuthor => this.GetBodyNode<GbxVehicleClass>()?.Author;
        public string VehicleCollection => this.GetBodyNode<GbxVehicleClass>()?.Collection;

        //Times & Scores
        public TimeSpan? BronzeTime => this.GetHeaderNode<GbxTmDescriptionClass>()?.BronzeTimeSpan ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Times?.BronzeTimeSpan;
        public TimeSpan? SilverTime => this.GetHeaderNode<GbxTmDescriptionClass>()?.SilverTimeSpan ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Times?.SilverTimeSpan;
        public TimeSpan? GoldTime => this.GetHeaderNode<GbxTmDescriptionClass>()?.GoldTimeSpan ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Times?.GoldTimeSpan;
        public TimeSpan? AuthorTime => this.GetHeaderNode<GbxTmDescriptionClass>()?.AuthorTimeSpan ?? this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Times?.AuthorTimeSpan;
        public int? AuthorScore => (int?)this.GetHeaderNode<GbxTmDescriptionClass>()?.AuthorScore ?? (int?)this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Times?.AuthorScore;

        //More Custom Vehicle data?

        //Environment/Decoration
        public string TimeOfDay => this.GetBodyNode<GbxMapClass>()?.TimeOfDay ?? this.GetHeaderNode<GbxCommonClass>()?.DecorationTimeOfDay;
        public string DecorationEnvironment => this.GetBodyNode<GbxMapClass>()?.DecorationEnvironment ?? this.GetHeaderNode<GbxCommonClass>()?.DecorationEnvironment;
        public string DecorationEnvironmentAuthor => this.GetBodyNode<GbxMapClass>()?.DecorationEnvironmentAuthor ?? this.GetHeaderNode<GbxCommonClass>()?.DecorationEnvironmentAuthor;
        public int? SizeX => (int?)this.GetBodyNode<GbxMapClass>()?.SizeX;
        public int? SizeY => (int?)this.GetBodyNode<GbxMapClass>()?.SizeY;
        public int? SizeZ => (int?)this.GetBodyNode<GbxMapClass>()?.SizeZ;
        public Vector2D? Origin => this.GetBodyNode<GbxMapCoordinateClass>()?.Origin ?? this.GetHeaderNode<GbxCommonClass>()?.MapOrigin;
        public Vector2D? Target => this.GetBodyNode<GbxMapCoordinateClass>()?.Target ?? this.GetHeaderNode<GbxCommonClass>()?.MapTarget;

        //Advanced
        public int? LightmapVersion => this.GetHeaderNode<GbxCommonClass>()?.LightmapVersion;
        public Block[] Blocks => this.GetBodyNode<GbxMapClass>()?.Blocks;
        public FileReference CustomMusic => this.GetBodyNode<GbxCustomMusicClass>()?.CustomMusic;
        public int? EmbeddedItemSize => this.GetBodyNode<GbxEmbeddedItemsClass>()?.ZipSize;
        public int? EmbeddedItemSizeInGame => (int?)this.GetBodyNode<GbxEmbeddedItemsClass>()?.EmbeddedItemSize;
        public GbxEmbeddedItem[] EmbeddedItems => this.GetBodyNode<GbxEmbeddedItemsClass>()?.Items;
        public GbxEmbeddedItemFile[] EmbeddedItemFiles => this.GetBodyNode<GbxEmbeddedItemsClass>()?.Files;
        public List<Dependency> Dependencies => this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.Dependencies?.Deps;
        public string ExecutableBuildDate => this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.ExecutableBuildDate;
        public string ExecutableVersion => this.GetHeaderNode<GbxMapCommunityClass>()?.Root?.ExecutableVersion;
    }
}
