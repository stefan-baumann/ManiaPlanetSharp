using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxThumbnailClass
        : GbxChallengeClass
    {
        public uint Version { get; set; }
        public byte[] ThumbnailData { get; set; }
        public string Comment { get; set; }
    }

    public class GbxThumbnailClassParser
        : GbxChallengeClassParser<GbxThumbnailClass>
    {
        protected override int Chunk => 7;

        protected override GbxThumbnailClass ParseChunk(GbxReader reader)
        {
            GbxThumbnailClass thumbnail = new GbxThumbnailClass();
            thumbnail.Version = reader.ReadUInt32();
            if (thumbnail.Version != 0)
            {
                uint thumbnailSize = reader.ReadUInt32();
                reader.ReadString("<Thumbnail.jpg>".Length);
                thumbnail.ThumbnailData = reader.ReadRaw((int)thumbnailSize);
                reader.ReadString("</Thumbnail.jpg>".Length);
                reader.ReadString("<Comments>".Length);
                thumbnail.Comment = reader.ReadString();
                reader.ReadString("</Comments>".Length);
            }

            return thumbnail;
        }
    }
}
