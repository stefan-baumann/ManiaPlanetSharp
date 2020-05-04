using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x090F4000)]
    public class GameSkinChunk
        : Chunk
    {
        [Property]
        public byte Version { get; set; }

        [Property]
        public string DirectoryName { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public string TextureName { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public string SceneId { get; set; }

        [Property, CustomParserMethod(nameof(ParseSkinFiles))]
        public SkinFile[] SkinFiles { get; set; }

        public SkinFile[] ParseSkinFiles(GameBoxReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            SkinFile[] skinFiles = new SkinFile[reader.ReadByte()];
            for (int i = 0; i < skinFiles.Length; i++)
            {
                skinFiles[i] = new SkinFile()
                {
                    ClassId = reader.ReadUInt32(),
                    Name = reader.ReadString(),
                    File = reader.ReadString()
                };
                if (this.Version >= 2)
                {
                    skinFiles[i].NeedsMipMap = reader.ReadBool();
                }
            }

            return skinFiles;
        }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 4)]
        public string AlternateDirectoryName { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 5)]
        public bool UseDefaultSkin { get; set; }
    }

    [CustomStruct]
    public class SkinFile
    {
        public uint ClassId { get; set; }

        public string Name { get; set; }

        public string File { get; set; }

        public bool NeedsMipMap { get; set; }
    }
}
