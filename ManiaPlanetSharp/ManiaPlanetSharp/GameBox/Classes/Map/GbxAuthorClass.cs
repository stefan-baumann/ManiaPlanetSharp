using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxAuthorClass
        : Node
    {
        public uint Version { get; set; }
        public uint AuthorVersion { get; set; }
        public string Login { get; set; }
        public string Nick { get; set; }
        public string Zone { get; set; }
        public string ExtraInfo { get; set; }
    }

    public class GbxAuthorClassParser
        : ClassParser<GbxAuthorClass>
    {
        protected override int ChunkId => 0x3043008;

        protected override GbxAuthorClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxAuthorClass()
            {
                Version = reader.ReadUInt32(),
                AuthorVersion = reader.ReadUInt32(),
                Login = reader.ReadString(),
                Nick = reader.ReadString(),
                Zone = reader.ReadString(),
                ExtraInfo = reader.ReadString()
            };
        }
    }
}
