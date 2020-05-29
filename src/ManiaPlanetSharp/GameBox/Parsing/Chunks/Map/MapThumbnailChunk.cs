using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x3043007)]
    public class MapThumbnailChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property, CustomParserMethod(nameof(ReadThumbnailData)), Condition(nameof(Version), ConditionOperator.GreaterThan, 0)]
        public byte[] ThumbnailData { get; set; }

        [Property, CustomParserMethod(nameof(ReadComment)), Condition(nameof(Version), ConditionOperator.GreaterThan, 0)]
        public string Comment { get; set; }

        public byte[] ReadThumbnailData(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            uint thumbnailSize = reader.ReadUInt32();
            reader.ReadString("<Thumbnail.jpg>".Length);
            var thumbnailData = reader.ReadRaw((int)thumbnailSize);
            reader.ReadString("</Thumbnail.jpg>".Length);
            return thumbnailData;
        }

        public string ReadComment(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            reader.ReadString("<Comments>".Length);
            var comment = reader.ReadString();
            reader.ReadString("</Comments>".Length);
            return comment;
        }
    }
}
