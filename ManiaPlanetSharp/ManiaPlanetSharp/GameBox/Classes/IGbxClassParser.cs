using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public interface IGbxClassParser<out TChunk>
        where TChunk : class
    {
        bool CanParse(uint chunkId);

        bool Skippable { get; }

        TChunk ParseChunk(GbxReader chunk);
    }
}
 