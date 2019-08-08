using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class ReferenceTable
        : ReferenceTableFolder
    {
        public ReferenceTable()
        {
            this.Name = "ReferenceTable";
        }

        public uint ExternalNodeCount { get; set; }
        public uint AncestorLevel { get; set; }
        public List<ReferenceTableExternalNode> ExternalNodes { get; set; } = new List<ReferenceTableExternalNode>();
    }

    public class ReferenceTableFolder
        : Node
    {
        public string Name { get; set; }
        public uint SubFolderCount { get; set; }
        public List<ReferenceTableFolder> SubFolders { get; set; } = new List<ReferenceTableFolder>();
    }

    public class ReferenceTableExternalNode
    {
        public uint Flags { get; set; }
        public bool HasFlag(int index)
        {
            return (this.Flags & (1UL << index)) > 0;
        }
        public string FileName { get; set; }
        public uint ResourceIndex { get; set; }
        public uint NodeIndex { get; set; }
        public bool UseFile { get; set; }
        public uint FolderIndex { get; set; }
    }
}
