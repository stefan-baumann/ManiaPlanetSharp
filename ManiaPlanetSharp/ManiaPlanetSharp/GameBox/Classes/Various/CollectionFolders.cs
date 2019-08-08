using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public class CollectionFolders
        : Node
    {
        public byte Version { get; set; }
        public string FolderBlockInfo { get; set; }
        public string FolderItem { get; set; }
        public string FolderDecoration { get; set; }
        public string Folder { get; set; }
        public string FolderCardEventInfo { get; set; }
        public string FolderMacroBlockInfo { get; set; }
        public string FolderMacroDecals { get; set; }
    }

    public class CollectionFoldersParser
        : ClassParser<CollectionFolders>
    {
        protected override int ChunkId => 0x03033002;

        protected override CollectionFolders ParseChunkInternal(GameBoxReader reader)
        {
            var result = new CollectionFolders()
            {
                Version =reader.ReadByte(),
                FolderBlockInfo = reader.ReadString(),
                FolderItem = reader.ReadString(),
                FolderDecoration = reader.ReadString()
            };
            if (result.Version <= 2)
            {
                result.Folder = reader.ReadString();
            }
            if (result.Version >= 2)
            {
                result.FolderCardEventInfo = reader.ReadString();
                if (result.Version >= 3)
                {
                    result.FolderMacroBlockInfo = reader.ReadString();
                    if (result.Version >= 4)
                    {
                        result.FolderMacroDecals = reader.ReadString();
                    }
                }
            }

            return result;
        }
    }
}
