using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x3043008)]
    public class AuthorChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property]
        public uint AuthorVersion { get; set; }

        [Property]
        public string Login { get; set; }

        [Property]
        public string Nick { get; set; }

        [Property]
        public string Zone { get; set; }

        [Property]
        public string ExtraInfo { get; set; }
    }
}
