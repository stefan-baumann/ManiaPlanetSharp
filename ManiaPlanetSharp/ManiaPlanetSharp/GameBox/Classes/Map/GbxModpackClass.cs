using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxModpackClass
        : Node
    {
        public FileReference Modpack { get; set; }
    }

    public class GbxModpackClassParser
        : ClassParser<GbxModpackClass>
    {
        protected override int ChunkId => 0x03043019;

        public override bool Skippable => true;

        protected override GbxModpackClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxModpackClass()
            {
                Modpack = reader.ReadFileReference()
            };
        }
    }
}
