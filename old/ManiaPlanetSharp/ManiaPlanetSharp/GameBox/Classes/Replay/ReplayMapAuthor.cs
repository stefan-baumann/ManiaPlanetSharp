using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Replay
{
    public class ReplayMapAuthor
        : Node
    {
        [AutoParserProperty(0)]
        public uint Version { get; set; }
        [AutoParserProperty(1)]
        public uint AuthorVersion { get; set; }
        [AutoParserStringProperty(2, false)]
        public string AuthorLogin { get; set; }
        [AutoParserStringProperty(3, false)]
        public string AuthorNick { get; set; }
        [AutoParserStringProperty(4, false)]
        public string AuthorZone { get; set; }
        [AutoParserStringProperty(5, false)]
        public string AuthorExtraInfo { get; set; }
    }
}
