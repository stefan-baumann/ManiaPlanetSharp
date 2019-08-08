using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxModpackClass
        : GbxClass
    {
        public GbxFileReference Modpack { get; set; }
    }

    public class GbxModpackClassParser
        : GbxClassParser<GbxModpackClass>
    {
        protected override int ChunkId => 0x03043019;

        public override bool Skippable => true;

        protected override GbxModpackClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxModpackClass()
            {
                Modpack = reader.ReadFileReference()
            };
        }
    }
}
