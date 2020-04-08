using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                this.xmlString = value;
                XmlSerializer serializer = new XmlSerializer(typeof(MapCommunityRoot));
                using (StringReader stringReader = new StringReader(this.xmlString.Replace("&", "&amp;")))
                {
                    this.Root = (MapCommunityRoot)serializer.Deserialize(stringReader);
                }
            }
        }

        public MapCommunityRoot Root { get; set; }
    }

    [XmlRoot(ElementName = "header")]
    public class MapCommunityRoot
    {
        [XmlElement(ElementName = "ident")]
        public MapCommunityIdentity Identity { get; set; }

        [XmlElement(ElementName = "desc")]
        public MapCommunityDescription Description { get; set; }

        [XmlElement(ElementName = "playermodel")]
        public MapCommunityPlayerModel PlayerModel { get; set; }

        [XmlElement(ElementName = "times")]
        public MapCommunityTimes Times { get; set; }

        [XmlElement(ElementName = "deps")]
        public MapCommunityDependencies Dependencies { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "exever")]
        public string ExecutableVersion { get; set; }

        [XmlAttribute(AttributeName = "exebuild")]
        public string ExecutableBuildDate { get; set; }

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "lightmap")]
        public int Lightmap { get; set; }
    }

    [XmlRoot(ElementName = "ident")]
    public class MapCommunityIdentity
    {
        [XmlAttribute(AttributeName = "uid")]
        public string Uid { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }
        [XmlAttribute(AttributeName = "authorzone")]
        public string AuthorZone { get; set; }
    }

    [XmlRoot(ElementName = "desc")]
    public class MapCommunityDescription
    {
        [XmlAttribute(AttributeName = "envir")]
        public string Environment { get; set; }
        [XmlAttribute(AttributeName = "mood")]
        public string Mood { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "maptype")]
        public string MapType { get; set; }
        [XmlAttribute(AttributeName = "mapstyle")]
        public string MapStyle { get; set; }
        [XmlAttribute(AttributeName = "validated")]
        //[Obsolete("Use the property Validated instead of ValidatedB.", false)] //The obsolete-attribute prevents the xml parser to parse
        public string ValidatedB { get; set; }
        [XmlIgnore()]
        public bool Validated { get => this.ValidatedB == "1"; }
        [XmlAttribute(AttributeName = "nblaps")]
        public int LapCount { get; set; }
        [XmlAttribute(AttributeName = "displaycost")]
        public int DisplayCost { get; set; }
        [XmlAttribute(AttributeName = "mod")]
        public string Mod { get; set; }
        [XmlAttribute(AttributeName = "hasghostblocks")]
        //[Obsolete("Use the property HasGhostBlocks instead of HasGhostBlocksB.", false)] //The obsolete-attribute prevents the xml parser to parse
        public string HasGhostBlocksB { get; set; }
        [XmlIgnore()]
        public bool HasGhostBlocks { get => this.HasGhostBlocksB == "1"; }
    }

    [XmlRoot(ElementName = "playermodel")]
    public class MapCommunityPlayerModel
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "times")]
    public class MapCommunityTimes
    {
        [XmlAttribute(AttributeName = "bronze")]
        public int Bronze { get; set; }
        [XmlIgnore()]
        public TimeSpan BronzeTimeSpan { get => TimeSpan.FromMilliseconds(this.Bronze); }
        [XmlAttribute(AttributeName = "silver")]
        public int Silver { get; set; }
        [XmlIgnore()]
        public TimeSpan SilverTimeSpan { get => TimeSpan.FromMilliseconds(this.Silver); }
        [XmlAttribute(AttributeName = "gold")]
        public int Gold { get; set; }
        [XmlIgnore()]
        public TimeSpan GoldTimeSpan { get => TimeSpan.FromMilliseconds(this.Gold); }
        [XmlAttribute(AttributeName = "authortime")]
        public int AuthorTime { get; set; }
        [XmlIgnore()]
        public TimeSpan AuthorTimeSpan { get => TimeSpan.FromMilliseconds(this.AuthorTime); }
        [XmlAttribute(AttributeName = "authorscore")]
        public int AuthorScore { get; set; }
    }

    [XmlRoot(ElementName = "deps")]
    public class MapCommunityDependencies
    {
        [XmlElement(ElementName = "dep")]
        public List<Dependency> Deps { get; set; }
    }

    [XmlRoot(ElementName = "dep")]
    public class Dependency
    {
        [XmlAttribute(AttributeName = "file")]
        public string File { get; set; }
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
    }
}
