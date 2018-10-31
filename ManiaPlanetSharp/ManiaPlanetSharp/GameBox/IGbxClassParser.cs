using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public interface IGbxClassParser<out TChunk>
        where TChunk : class
    {
        bool CanParse(int chunkId);

        //TChunk ParseChunk(GbxChunk chunk);
    }

    public interface IGbxChallengeClassParser<out TChunk>
        : IGbxClassParser<TChunk>
        where TChunk : class
    {
        TChunk ParseChunk(GbxChunk chunk);
    }

    public interface IGbxBodyClassParser<out TChunk>
        : IGbxClassParser<TChunk>
        where TChunk : class
    {
        bool Skippable { get; }

        TChunk ParseChunk(GbxReader chunk);
    }
}
