using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxCustomMusicClass
        : Node
    {
        public FileReference CustomMusic { get; set; }
    }

    public class GbxCustomMusicClassParser
        : ClassParser<GbxCustomMusicClass>
    {
        protected override int ChunkId => 0x03043024;

        protected override GbxCustomMusicClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxCustomMusicClass()
            {
                CustomMusic = reader.ReadFileReference()
            };
        }
    }
}
