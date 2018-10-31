using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxAuthorClass
        : GbxChallengeClass
    {
        public uint Version { get; set; }
        public uint AuthorVersion { get; set; }
        public string Login { get; set; }
        public string Nick { get; set; }
        public string Zone { get; set; }
        public string ExtraInfo { get; set; }
    }

    public class GbxAuthorClassParser
        : GbxChallengeClassParser<GbxAuthorClass>
    {
        protected override int Chunk => 8;

        protected override GbxAuthorClass ParseChunk(GbxReader reader)
        {
            GbxAuthorClass author = new GbxAuthorClass();
            author.Version = reader.ReadUInt32();
            author.AuthorVersion = reader.ReadUInt32();
            author.Login = reader.ReadString();
            author.Nick = reader.ReadString();
            author.Zone = reader.ReadString();
            author.ExtraInfo = reader.ReadString();

            return author;
        }
    }
}
