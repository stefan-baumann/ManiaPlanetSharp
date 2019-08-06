using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCustomMusicClass
        : GbxBodyClass
    {
        public GbxFileReference CustomMusic { get; set; }
    }

    public class GbxCustomMusicClassParser
        : GbxBodyClassParser<GbxCustomMusicClass>
    {
        protected override int Chunk => 0x03043024;

        protected override GbxCustomMusicClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxCustomMusicClass()
            {
                CustomMusic = reader.ReadFileReference()
            };
        }
    }
}
