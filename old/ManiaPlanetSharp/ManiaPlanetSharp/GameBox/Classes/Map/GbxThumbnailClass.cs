﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxThumbnailClass
        : Node
    {
        public uint Version { get; set; }
        public byte[] ThumbnailData { get; set; }
        public string Comment { get; set; }
    }

    public class GbxThumbnailClassParser
        : ClassParser<GbxThumbnailClass>
    {
        protected override int ChunkId => 0x3043007;

        protected override GbxThumbnailClass ParseChunkInternal(GameBoxReader reader)
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
