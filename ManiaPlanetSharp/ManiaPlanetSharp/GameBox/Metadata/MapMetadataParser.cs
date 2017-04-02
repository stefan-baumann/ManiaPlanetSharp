using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace ManiaPlanetSharp.GameBox.Metadata
{
    /// <summary>
    /// Parses the metadata of the specified GameBox map.
    /// </summary>
    public class MapMetadataParser
        : GameBoxParser<MapMetadata>
    {
        public MapMetadataParser(Stream stream)
            : base(stream)
        { }
        
        public override MapMetadata Parse()
        {
            if (this.ClassId != 0x03043000)
            {
                throw new InvalidDataException("The supplied stream does not contain a valid ManiaPlanet map gamebox file.");
            }

            //Parse the data of all relevant chunks
            MapMetadata result = new MapMetadata();
            foreach (GameBoxChunk chunk in this.Chunks)
            {
                switch (chunk.Id & 0xfff)
                {
                    case 5:
                        using (MemoryStream chunkStream = new MemoryStream(chunk.Data))
                        {
                            this.ReadChunk5(new GameBoxStreamReader(chunkStream), result);
                        }
                        break;
                    case 7:
                        //The thumbnail is ignored for now
                        break;
                    case 8:
                        using (MemoryStream chunkStream = new MemoryStream(chunk.Data))
                        {
                            this.ReadChunk8(new GameBoxStreamReader(chunkStream), result);
                        }
                        break;
                }
            }

            return result;
        }

        private void ReadChunk5(GameBoxStreamReader reader, MapMetadata mapMetadata)
        {
            //Parse the XML
            string xmlString = reader.ReadString();
            xmlString = xmlString.Replace("&", "&amp;"); //For whatever reason maniaplanet produces xml data with unescaped '&'-signs
            XDocument xml = XDocument.Parse(xmlString);
            XElement header = xml.Element("header");
            XElement ident = header.Element("ident");
            XElement desc = header.Element("desc");
            XElement times = header.Element("times");

            //Parse all the values given in the xml data
            mapMetadata.Title = header.Attribute("title")?.Value;
            mapMetadata.Uid = ident.Attribute("uid")?.Value;
            mapMetadata.Name = ident.Attribute("name")?.Value;
            mapMetadata.AuthorLogin = ident.Attribute("author")?.Value;
            mapMetadata.AuthorZone = ident.Attribute("authorzone")?.Value;
            mapMetadata.Environment = desc.Attribute("envir")?.Value;
            mapMetadata.Mood = desc.Attribute("mood")?.Value;
            mapMetadata.Type = desc.Attribute("type")?.Value;
            if (mapMetadata.Type == "Script")
            {
                mapMetadata.Type = desc.Attribute("maptype")?.Value;
            }
            mapMetadata.MapStyle = desc.Attribute("mapstyle")?.Value;
            mapMetadata.Validated = desc.Attribute("validated")?.Value == "1";
            mapMetadata.LapCount = int.Parse(desc.Attribute("nblaps")?.Value);
            mapMetadata.DisplayCost = int.Parse(desc.Attribute("displaycost")?.Value);
            mapMetadata.Mod = desc.Attribute("mod")?.Value;
            mapMetadata.HasGhostblocks = desc.Attribute("hasghostblocks")?.Value == "1";
            mapMetadata.AuthorScore = int.Parse(times.Attribute("authorscore")?.Value);
            mapMetadata.AuthorTime = TimeSpan.FromMilliseconds(int.Parse(times.Attribute("authortime")?.Value));
            mapMetadata.GoldTime = TimeSpan.FromMilliseconds(int.Parse(times.Attribute("gold")?.Value));
            mapMetadata.SilverTime = TimeSpan.FromMilliseconds(int.Parse(times.Attribute("silver")?.Value));
            mapMetadata.BronzeTime = TimeSpan.FromMilliseconds(int.Parse(times.Attribute("bronze")?.Value));
        }

        //private void ReadChunk7(GameBoxStreamReader reader, MapMetadata mapMetadata)
        //{
        //    //Check if a thumbnail exists
        //    if (reader.ReadLong() == 1)
        //    {
        //        int thumbnailSize = reader.ReadLong();
        //        reader.Skip("<Thumbnail.jpg>".Length);
        //        if (thumbnailSize > 0)
        //        {
        //            //Parse Thumbnail
        //            byte[] thumbnailBuffer = reader.ReadRaw(thumbnailSize);
        //            using (MemoryStream thumbnailDataStream = new MemoryStream(thumbnailBuffer))
        //            {
        //                mapMetadata.Thumbnail = Bitmap.FromStream(thumbnailDataStream);
        //                mapMetadata.Thumbnail.RotateFlip(RotateFlipType.Rotate180FlipX);
        //            }
        //        }
        //
        //        //Parse Comment
        //        reader.Skip("</Thumbnail.jpg><Comments>".Length);
        //        mapMetadata.Comment = reader.ReadString();
        //        reader.Skip("</Comments>".Length);
        //    }
        //}

        private void ReadChunk8(GameBoxStreamReader reader, MapMetadata mapMetadata)
        {
            reader.Skip(8); //Skip unneeded data

            //Parse Author information
            mapMetadata.AuthorLogin = reader.ReadString();
            mapMetadata.AuthorName = reader.ReadString();
            mapMetadata.AuthorZone = reader.ReadString();
            mapMetadata.AuthorExtra = reader.ReadString();
        }
    }
}
