using System;
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
                    this.Root = this.ParseXmlString(this.xmlString.Replace("&", "&amp;"));
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
                Lightmap = int.TryParse(document.Root.Attribute("lightmap")?.Value ?? string.Empty, out int i0) ? i0 : 0
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
                    Environment = identity.Attribute("envir")?.Value,
                    Mood = identity.Attribute("mood")?.Value,
                    Type = identity.Attribute("type")?.Value,
                    MapType = identity.Attribute("maptype")?.Value,
                    MapStyle = identity.Attribute("mapstyle")?.Value,
                    Validated = identity.Attribute("validated")?.Value == "1",
                    LapCount = int.TryParse(identity.Attribute("nblaps")?.Value ?? string.Empty, out int i1) ? i1 : 0,
                    DisplayCost = int.TryParse(identity.Attribute("displaycost")?.Value ?? string.Empty, out int i2) ? i2 : 0,
                    Mod = identity.Attribute("mod")?.Value,
                    HasGhostBlocks = identity.Attribute("hasghostblocks")?.Value == "1"
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
                    Bronze = int.TryParse(identity.Attribute("bronze")?.Value ?? string.Empty, out int i1) ? i1 : 0,
                    Silver = int.TryParse(identity.Attribute("silver")?.Value ?? string.Empty, out int i2) ? i2 : 0,
                    Gold = int.TryParse(identity.Attribute("gold")?.Value ?? string.Empty, out int i3) ? i3 : 0,
                    AuthorTime = int.TryParse(identity.Attribute("authortime")?.Value ?? string.Empty, out int i4) ? i4 : 0,
                    AuthorScore = int.TryParse(identity.Attribute("authorscore")?.Value ?? string.Empty, out int i5) ? i5 : 0,
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

        public int Lightmap { get; set; }
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

        public bool Validated { get; set; }

        public int LapCount { get; set; }

        public int DisplayCost { get; set; }

        public string Mod { get; set; }

        public bool HasGhostBlocks { get; set; }
    }

    public class MapCommunityPlayerModel
    {
        public string Id { get; set; }
    }

    public class MapCommunityTimes
    {
        public int Bronze { get; set; }

        public TimeSpan BronzeTimeSpan { get => TimeSpan.FromMilliseconds(this.Bronze); }

        public int Silver { get; set; }

        public TimeSpan SilverTimeSpan { get => TimeSpan.FromMilliseconds(this.Silver); }

        public int Gold { get; set; }

        public TimeSpan GoldTimeSpan { get => TimeSpan.FromMilliseconds(this.Gold); }

        public int AuthorTime { get; set; }

        public TimeSpan AuthorTimeSpan { get => TimeSpan.FromMilliseconds(this.AuthorTime); }

        public int AuthorScore { get; set; }
    }

    public class MapCommunityDependency
    {
        public string File { get; set; }

        public string Url { get; set; }
    }
}
