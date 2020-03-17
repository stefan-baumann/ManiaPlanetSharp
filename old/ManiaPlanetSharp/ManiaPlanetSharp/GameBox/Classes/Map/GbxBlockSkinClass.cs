using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxBlockSkinClass
        : Node
    {
        public string Text { get; set; }
        public string Ignored { get; set; }
        public FileReference Pack { get; set; }
        public FileReference ParentPack { get; set; }
    }

    public class GbxBlockSkinClassParserA
        : ClassParser<GbxBlockSkinClass>
    {
        protected override int ChunkId => 0x03059000;

        protected override GbxBlockSkinClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxBlockSkinClass()
            {
                Text = reader.ReadString(),
                Ignored = reader.ReadString()
            };
        }
    }

    public class GbxBlockSkinClassParserB
        : ClassParser<GbxBlockSkinClass>
    {
        protected override int ChunkId => 0x03059001;

        protected override GbxBlockSkinClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxBlockSkinClass()
            {
                Text = reader.ReadString(),
                Pack = reader.ReadFileReference()
            };
        }
    }

    public class GbxBlockSkinClassParserC
        : ClassParser<GbxBlockSkinClass>
    {
        protected override int ChunkId => 0x03059002;

        protected override GbxBlockSkinClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxBlockSkinClass()
            {
                Text = reader.ReadString(),
                Pack = reader.ReadFileReference(),
                ParentPack = reader.ReadFileReference()
            };
        }
    }
}
