using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCustomMusicClass
        : GbxClass
    {
        public GbxFileReference CustomMusic { get; set; }
    }

    public class GbxCustomMusicClassParser
        : GbxClassParser<GbxCustomMusicClass>
    {
        protected override int ChunkId => 0x03043024;

        protected override GbxCustomMusicClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxCustomMusicClass()
            {
                CustomMusic = reader.ReadFileReference()
            };
        }
    }
}
