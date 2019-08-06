using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxNadeoSkinFidsClass
        : GbxBodyClass
    {
        public uint NadeoSkinFidCount { get; set; }
        public GbxFileReference[] Fids { get; set; }
    }

    public class GbxNadeoSkinFidsClassParser
        : GbxBodyClassParser<GbxNadeoSkinFidsClass>
    {
        protected override int Chunk => 0x2E002008;

        protected override GbxNadeoSkinFidsClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxNadeoSkinFidsClass();
            result.NadeoSkinFidCount = reader.ReadUInt32();
            result.Fids = new GbxFileReference[result.NadeoSkinFidCount];
            for (int i = 0; i < result.NadeoSkinFidCount; i++)
            {
                result.Fids[i] = reader.ReadFileRef();
            }
            return result;
        }
    }
}
