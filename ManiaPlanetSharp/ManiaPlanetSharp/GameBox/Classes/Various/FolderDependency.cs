using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public class FolderDependency
        : Node
    {
        [AutoParserProperty(0)]
        public uint DirectoryCount { get; set; }
        [AutoParserArrayProperty(1, 0)]
        public string[] Directories { get; set; }
    }
}
