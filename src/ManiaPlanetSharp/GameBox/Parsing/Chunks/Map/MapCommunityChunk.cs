﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x3043005)]
    public class MapCommunityChunk
        : Chunk
    {
        private string xmlString;
        [Property]
        public string XmlString
        {
            get
            {
                return this.xmlString;
            }
            set
            {
                if (this.xmlString != value)
                {
                    this.xmlString = value;
                    if (this.xmlString != null)
                    {
                        this.Root = this.ParseXmlString(this.xmlString.Replace("&", "&amp;"));
                    }
                }
            }
        }

        private MapCommunityRoot ParseXmlString(string xmlString)
        {
            XDocument document = XDocument.Parse(xmlString);
            if (document.Root.Name != "header")
            {
                return null;
            }

            MapCommunityRoot root = new MapCommunityRoot()
            {
                Type = document.Root.Attribute("type")?.Value,
                ExecutableVersion = document.Root.Attribute("exever")?.Value,
                ExecutableBuildDate = document.Root.Attribute("exebuild")?.Value,
                Title = document.Root.Attribute("title")?.Value,
                Lightmap = int.TryParse(document.Root.Attribute("lightmap")?.Value ?? string.Empty, out int i0) ? (int?)i0 : null
            };

            var identity = document.Root.Element("ident");
            if (identity != null)
            {
                root.Identity = new MapCommunityIdentity()
                {
                    Uid = identity.Attribute("uid")?.Value,
                    Name = identity.Attribute("name")?.Value,
                    Author = identity.Attribute("author")?.Value,
                    AuthorZone = identity.Attribute("authorzone")?.Value,
                };
            }

            var description = document.Root.Element("desc");
            if (description != null)
            {
                root.Description = new MapCommunityDescription()
                {
                    Environment = description.Attribute("envir")?.Value,
                    Mood = description.Attribute("mood")?.Value,
                    Type = description.Attribute("type")?.Value,
                    MapType = description.Attribute("maptype")?.Value,
                    MapStyle = description.Attribute("mapstyle")?.Value,
                    Validated = description.Attribute("validated") != null ? (bool?)(description.Attribute("validated").Value == "1") : null,
                    LapCount = int.TryParse(description.Attribute("nblaps")?.Value ?? string.Empty, out int i1) ? (int?)i1 : null,
                    DisplayCost = int.TryParse(description.Attribute("displaycost")?.Value ?? string.Empty, out int i2) ? (int?)i2 : null,
                    Mod = description.Attribute("mod")?.Value,
                    HasGhostBlocks = description.Attribute("hasghostblocks") != null ? (bool?)(description.Attribute("hasghostblocks").Value == "1") : null
                };
            }

            var playermodel = document.Root.Element("playermodel");
            if (playermodel != null)
            {
                root.PlayerModel = new MapCommunityPlayerModel()
                {
                    Id = playermodel.Attribute("id")?.Value
                };
            }

            var times = document.Root.Element("times");
            if (times != null)
            {
                root.Times = new MapCommunityTimes()
                {
                    Bronze = int.TryParse(times.Attribute("bronze")?.Value ?? string.Empty, out int i1) ? (int?)i1 : null,
                    Silver = int.TryParse(times.Attribute("silver")?.Value ?? string.Empty, out int i2) ? (int?)i2 : null,
                    Gold = int.TryParse(times.Attribute("gold")?.Value ?? string.Empty, out int i3) ? (int?)i3 : null,
                    AuthorTime = int.TryParse(times.Attribute("authortime")?.Value ?? string.Empty, out int i4) ? (int?)i4 : null,
                    AuthorScore = int.TryParse(times.Attribute("authorscore")?.Value ?? string.Empty, out int i5) ? (int?)i5 : null,
                };
            }

            var dependencies = document.Root.Element("deps");
            if (dependencies != null)
            {
                root.Dependencies = dependencies.Elements("dep").Select(dep => new MapCommunityDependency()
                {
                    File = dep.Attribute("file")?.Value,
                    Url = dep.Attribute("url")?.Value
                }).ToList();
            }
            else root.Dependencies = new List<MapCommunityDependency>();

            return root;
        }

        public MapCommunityRoot Root { get; private set; }
    }

    public class MapCommunityRoot
    {
        public MapCommunityIdentity Identity { get; set; }

        public MapCommunityDescription Description { get; set; }

        public MapCommunityPlayerModel PlayerModel { get; set; }

        public MapCommunityTimes Times { get; set; }

        public List<MapCommunityDependency> Dependencies { get; set; }

        public string Type { get; set; }

        public string ExecutableVersion { get; set; }

        public string ExecutableBuildDate { get; set; }

        public string Title { get; set; }

        public int? Lightmap { get; set; }
    }

    public class MapCommunityIdentity
    {
        public string Uid { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string AuthorZone { get; set; }
    }

    public class MapCommunityDescription
    {
        public string Environment { get; set; }

        public string Mood { get; set; }

        public string Type { get; set; }

        public string MapType { get; set; }

        public string MapStyle { get; set; }

        public bool? Validated { get; set; }

        public int? LapCount { get; set; }

        public int? DisplayCost { get; set; }

        public string Mod { get; set; }

        public bool? HasGhostBlocks { get; set; }
    }

    public class MapCommunityPlayerModel
    {
        public string Id { get; set; }
    }

    public class MapCommunityTimes
    {
        public int? Bronze { get; set; }

        public TimeSpan? BronzeTimeSpan { get => this.Bronze != null && this.Bronze != -1 ? (TimeSpan?)TimeSpan.FromMilliseconds(this.Bronze.Value) : null; }

        public int? Silver { get; set; }

        public TimeSpan? SilverTimeSpan { get => this.Silver != null && this.Silver != -1 ? (TimeSpan?)TimeSpan.FromMilliseconds(this.Silver.Value) : null; }

        public int? Gold { get; set; }

        public TimeSpan? GoldTimeSpan { get => this.Gold != null && this.Gold != -1 ? (TimeSpan?)TimeSpan.FromMilliseconds(this.Gold.Value) : null; }

        public int? AuthorTime { get; set; }

        public TimeSpan? AuthorTimeSpan { get => this.AuthorTime != null && this.AuthorTime != -1 ? (TimeSpan?)TimeSpan.FromMilliseconds(this.AuthorTime.Value) : null; }

        public int? AuthorScore { get; set; }
    }

    public class MapCommunityDependency
    {
        public string File { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
        public string Url { get; set; }
    }
}
