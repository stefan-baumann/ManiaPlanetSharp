using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093001)]
    public class ReplayCommunityChunk
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
                XmlSerializer serializer = new XmlSerializer(typeof(ReplayCommunityRoot));
                using (StringReader stringReader = new StringReader(this.xmlString.Replace("&", "&amp;")))
                {
                    this.Root = (ReplayCommunityRoot)serializer.Deserialize(stringReader);
                }
            }
        }

        public ReplayCommunityRoot Root { get; set; }
    }

	[XmlRoot(ElementName = "map")]
	public class ReplayCommunityMap
	{
		[XmlAttribute(AttributeName = "author")]
		public string Author { get; set; }

		[XmlAttribute(AttributeName = "authorzone")]
		public string AuthorZone { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "uid")]
		public string Uid { get; set; }
	}

	[XmlRoot(ElementName = "desc")]
	public class ReplayCommunityDescription
	{
		[XmlAttribute(AttributeName = "displaycost")]
		public int DisplayCost { get; set; }

		[XmlAttribute(AttributeName = "envir")]
		public string Environment { get; set; }

		[XmlAttribute(AttributeName = "mapstyle")]
		public string MapStyle { get; set; }

		[XmlAttribute(AttributeName = "maptype")]
		public string MapType { get; set; }

		[XmlAttribute(AttributeName = "mod")]
		public string Mod { get; set; }

		[XmlAttribute(AttributeName = "mood")]
		public string Mood { get; set; }
	}

	[XmlRoot(ElementName = "playermodel")]
	public class ReplayCommunityPlayermodel
	{
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName = "times")]
	public class ReplayCommunityTimes
	{
		[XmlAttribute(AttributeName = "best")]
		public int Best { get; set; }

		[XmlIgnore()]
		public TimeSpan BestTimeSpan { get => TimeSpan.FromMilliseconds(this.Best); }

		[XmlAttribute(AttributeName = "respawns")]
		public int Respawns { get; set; }

		[XmlAttribute(AttributeName = "stuntscore")]
		public int StuntScore { get; set; }

		[XmlAttribute(AttributeName = "validable")]
		public int Validable { get; set; }

		[XmlIgnore()]
		public bool ValidableBoolean { get => this.Validable == 1; }
	}

	[XmlRoot(ElementName = "checkpoints")]
	public class ReplayCommunityCheckpoints
	{
		[XmlAttribute(AttributeName = "cur")]
		public int Current { get; set; }

		[XmlAttribute(AttributeName = "onelap")]
		public int OneLap { get; set; }
	}

	[XmlRoot(ElementName = "header")]
	public class ReplayCommunityRoot
	{
		[XmlElement(ElementName = "map")]
		public ReplayCommunityMap Map { get; set; }

		[XmlElement(ElementName = "desc")]
		public ReplayCommunityDescription Desciption { get; set; }

		[XmlElement(ElementName = "playermodel")]
		public ReplayCommunityPlayermodel Playermodel { get; set; }

		[XmlElement(ElementName = "times")]
		public ReplayCommunityTimes Times { get; set; }

		[XmlElement(ElementName = "checkpoints")]
		public ReplayCommunityCheckpoints Checkpoints { get; set; }

		[XmlAttribute(AttributeName = "exebuild")]
		public string ExecutableBuild { get; set; }

		[XmlAttribute(AttributeName = "exever")]
		public string ExecutableVersion { get; set; }

		[XmlAttribute(AttributeName = "title")]
		public string Title { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
	}
}
