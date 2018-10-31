using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCommunityClass
        : GbxChallengeClass
    {
        public string XmlString { get; set; }

        public CommunityRoot Root { get; set; }
    }

    [XmlRoot(ElementName = "header")]
    public class CommunityRoot
    {
        [XmlElement(ElementName = "ident")]
        public Identity Identity { get; set; }
        [XmlElement(ElementName = "desc")]
        public Description Description { get; set; }
        [XmlElement(ElementName = "playermodel")]
        public PlayerModel PlayerModel { get; set; }
        [XmlElement(ElementName = "times")]
        public Times Times { get; set; }
        [XmlElement(ElementName = "deps")]
        public Dependencies Dependencies { get; set; }
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
    public class Identity
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
    public class Description
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
        [Obsolete("Use the property Validated instead of ValidatedB.", false)]
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
        [Obsolete("Use the property HasGhostBlocks instead of HasGhostBlocksB.", false)]
        public string HasGhostBlocksB { get; set; }
        [XmlIgnore()]
        public bool HasGhostBlocks { get => this.HasGhostBlocksB == "1"; }
    }

    //Todo: Find out 
    [XmlRoot(ElementName = "playermodel")]
    public class PlayerModel
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "times")]
    public class Times
    {
        [XmlAttribute(AttributeName = "bronze")]
        public int Bronze { get; set; }
        [XmlAttribute(AttributeName = "silver")]
        public int Silver { get; set; }
        [XmlAttribute(AttributeName = "gold")]
        public int Gold { get; set; }
        [XmlAttribute(AttributeName = "authortime")]
        public int AuthorTime { get; set; }
        [XmlAttribute(AttributeName = "authorscore")]
        public int AuthorScore { get; set; }
    }

    [XmlRoot(ElementName = "deps")]
    public class Dependencies
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
    
    public class GbxCommunityClassParser
        : GbxChallengeClassParser<GbxCommunityClass>
    {
        protected override int Chunk => 5;

        public override GbxCommunityClass ParseChunk(GbxReader reader)
        {
            string xmlString = reader.ReadString();
            XmlSerializer serializer = new XmlSerializer(typeof(CommunityRoot));
            using (StringReader stringReader = new StringReader(xmlString.Replace("&", "&amp;")))
            {
                CommunityRoot root = (CommunityRoot)serializer.Deserialize(stringReader);
                return new GbxCommunityClass()
                {
                    Root = root,
                    XmlString = xmlString
                };
            }
        }
    }
}
