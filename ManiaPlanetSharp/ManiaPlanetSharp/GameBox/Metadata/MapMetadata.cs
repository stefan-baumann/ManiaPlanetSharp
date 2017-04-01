using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Metadata
{
    public class MapMetadata
    {
        public string Name { get; set; }
        public string AuthorLogin { get; set; }
        public string AuthorName { get; set; }
        public string AuthorZone { get; set; }
        public string AuthorExtra { get; set; }
        public bool Validated { get; set; }
        public int LapCount { get; set; }
        public string Comment { get; set; }
        //public Image Thumbnail { get; set; }
        public string Uid { get; set; }

        public string Title { get; set; }
        public string Environment { get; set; }
        public string Mood { get; set; }
        public string Mod { get; set; }
        public int DisplayCost { get; set; }
        public string Type { get; set; }
        public string MapStyle { get; set; }
        public bool HasGhostblocks { get; set; }

        public int AuthorScore { get; set; }
        public TimeSpan AuthorTime { get; set; }
        public TimeSpan GoldTime { get; set; }
        public TimeSpan SilverTime { get; set; }
        public TimeSpan BronzeTime { get; set; }
    }
}
