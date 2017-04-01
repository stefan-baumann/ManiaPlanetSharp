using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.ManiaExchange
{
    public class TrackInfo
    {
        public TrackInfo()
        { }

        public int TrackID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UploadedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string MapType { get; set; }
        public string TitlePack { get; set; }
        public string StyleName { get; set; }
        public string Mood { get; set; }
        public int DisplayCost { get; set; }
        public string ModName { get; set; }
        public int Lightmap { get; set; }
        public string ExeVersion { get; set; }
        public string ExeBuild { get; set; }
        public string EnvironmentName { get; set; }
        public string VehicleName { get; set; }
        public string RouteName { get; set; }
        public string LengthName { get; set; }
        public int Laps { get; set; }
        public string DifficultyName { get; set; }
        public int ReplayWRID { get; set; }
        public int ReplayCount { get; set; }
        public int TrackValue { get; set; }
        public string Comments { get; set; }
        public int AwardCount { get; set; }
        public int CommentCount { get; set; }
        public bool UnlimiterRequired { get; set; }
        public string TrackUID { get; set; }
        public bool Unreleased { get; set; }
        public string GbxMapName { get; set; }
        public int RatingVoteCount { get; set; }
        public double RatingVoteAverage { get; set; }
        public bool HasScreenshot { get; set; }
        public bool HasThumbnail { get; set; }
        public bool HasGhostBlocks { get; set; }
        public int EmbeddedObjectsCount { get; set; }

        public static implicit operator int(TrackInfo track)
        {
            return track.TrackID;
        }
    }
}
