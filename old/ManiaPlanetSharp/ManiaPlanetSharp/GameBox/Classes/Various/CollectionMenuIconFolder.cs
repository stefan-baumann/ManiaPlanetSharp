using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public class CollectionMenuIconFolder
        : Node
    {
        [AutoParserProperty(0)]
        public byte Version { get; set; }
        [AutoParserStringProperty(1, false)]
        public string FolderMenuIcon { get; set; }
    }
}
