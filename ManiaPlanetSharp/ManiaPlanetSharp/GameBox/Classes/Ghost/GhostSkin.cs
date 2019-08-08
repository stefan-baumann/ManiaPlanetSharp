using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public class GhostSkin
        : Node
    {
        [AutoParserProperty(0)]
        public uint SkinPackCount { get; set; }
        [AutoParserArrayProperty(1, 0)]
        public FileReference[] SkinPacks { get; set; }
        [AutoParserStringProperty(2, false)]
        public string GhostNickname { get; set; }
        [AutoParserStringProperty(3, false)]
        public string GhostAvatarName { get; set; }
    }
}
