using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    //[Chunk(0x03043011)]
    public class MapChallengeParameterChunk
        : Chunk
    {
        [Property]
        public Node CollectorList { get; set; }

        [Property]
        public Node ChallengeParameters { get; set; }

        [Property]
        [Obsolete("Raw Value, use GbxChallengeParameterClass.Kind instead", false)]
        public uint KindU { get; set; }

        public MapKind Kind { get => (MapKind)(byte)this.KindU; }
    }

    public enum MapKind
    : byte
    {
        EndMarker = 0,
        [Obsolete("", false)]
        CampaignOld = 1,
        [Obsolete("", false)]
        Puzzle = 2,
        [Obsolete("", false)]
        Retro = 3,
        [Obsolete("", false)]
        TimeAttack = 4,
        [Obsolete("", false)]
        Rounds = 5,
        InProgress = 6,
        Campaign = 7,
        Multi = 8,
        Solo = 9,
        Site = 10,
        SoloNadeo = 11,
        MultiNadeo = 12
    }
}
