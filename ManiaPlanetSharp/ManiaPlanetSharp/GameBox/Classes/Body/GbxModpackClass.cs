using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxModpackClass
        : GbxBodyClass
    {
        public GbxFileReference Modpack { get; set; }
    }

    public class GbxModpackClassParser
        : GbxBodyClassParser<GbxModpackClass>
    {
        protected override int Chunk => 0x03043019;

        public override bool Skippable => true;

        protected override GbxModpackClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxModpackClass()
            {
                Modpack = reader.ReadFileRef()
            };
        }
    }
}
