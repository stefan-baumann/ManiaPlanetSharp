using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public class DecorationMoodRemapping
        : Node
    {
        public byte Version { get; set; }
        public string DiretoryName { get; set; }
        public GameSkin Skin { get; set; }
    }

    public class DecorationMoodRemappingParser
        : ClassParser<DecorationMoodRemapping>
    {
        protected override int ChunkId => 0x03038000;

        protected override DecorationMoodRemapping ParseChunkInternal(GameBoxReader reader)
        {
            return new DecorationMoodRemapping()
            {
                Version = reader.ReadByte(),
                DiretoryName = reader.ReadString(),
                Skin = new GameSkinParser().ParseChunk(reader)
            };
        }
    }
}
