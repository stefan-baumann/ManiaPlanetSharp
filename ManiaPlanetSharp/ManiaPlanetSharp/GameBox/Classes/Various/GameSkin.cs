using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public struct SkinFile
    {
        public uint ClassId { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public bool NeedsMipMap { get; set; }
        public string AlternateDirectoryName { get; set; }
        public bool UseDefaultSkin { get; set; }
    }

    public class GameSkin
        : Node
    {
        public byte Version { get; set; }
        public string DirectoryName { get; set; }
        public string TextureName { get; set; }
        public string SceneId { get; set; }
        public byte SkinFileCount { get; set; }
        public SkinFile[] SkinFiles { get; set; }
    }

    public class GameSkinParser
        : ClassParser<GameSkin>
    {
        protected override int ChunkId => 0x090F4000;

        protected override GameSkin ParseChunkInternal(GameBoxReader reader)
        {
            var result = new GameSkin()
            {
                Version = reader.ReadByte(),
                DirectoryName = reader.ReadString()
            };
            if (result.Version >= 1)
            {
                result.TextureName = reader.ReadString();
                result.SceneId = reader.ReadString();
            }
            result.SkinFileCount = reader.ReadByte();
            result.SkinFiles = new SkinFile[result.SkinFileCount];
            for (int i = 0; i < result.SkinFileCount; i++)
            {
                result.SkinFiles[i] = new SkinFile()
                {
                    ClassId = reader.ReadUInt32(),
                    Name = reader.ReadString(),
                    File = reader.ReadString()
                };
                if (result.Version >= 4)
                {
                    result.SkinFiles[i].AlternateDirectoryName = reader.ReadString();
                    if (result.Version >= 5)
                    {
                        result.SkinFiles[i].UseDefaultSkin = reader.ReadBool();
                    }
                }
            }

            return result;
        }
    }
}
