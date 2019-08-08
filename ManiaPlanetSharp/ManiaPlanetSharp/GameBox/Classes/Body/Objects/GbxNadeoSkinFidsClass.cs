﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxNadeoSkinFidsClass
        : GbxClass
    {
        public uint NadeoSkinFidCount { get; set; }
        public GbxNode[] Fids { get; set; }
    }

    public class GbxNadeoSkinFidsClassParser
        : GbxClassParser<GbxNadeoSkinFidsClass>
    {
        protected override int ChunkId => 0x2E002008;

        protected override GbxNadeoSkinFidsClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxNadeoSkinFidsClass();
            result.NadeoSkinFidCount = reader.ReadUInt32();
            result.Fids = new GbxNode[result.NadeoSkinFidCount];
            for (int i = 0; i < result.NadeoSkinFidCount; i++)
            {
                result.Fids[i] = reader.ReadNodeReference();
            }
            return result;
        }
    }
}
