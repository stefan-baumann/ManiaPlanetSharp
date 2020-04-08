using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093002)]
    public class ReplayAuthorChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property]
        public uint AuthorVersion { get; set; }

        [Property]
        public string Login { get; set; }

        [Property]
        public string Nickname { get; set; }

        [Property]
        public string Zone { get; set; }

        [Property]
        public string ExtraInfo { get; set; }
    }
}
