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
                //XmlSerializer serializer = new XmlSerializer(typeof(ReplayCommunityRoot));
                //using (StringReader stringReader = new StringReader(this.xmlString.Replace("&", "&amp;")))
                //{
                //    this.Root = (ReplayCommunityRoot)serializer.Deserialize(stringReader);
                //}
            }
        }

        //public ReplayCommunityRoot Root { get; set; }
    }
}
