﻿using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxTimeLimitClass
        : Node
    {
        [Obsolete("Raw Value, use GbxTimeLimitClass.TimeLimit instead", false)]
        public uint TimeLimitU { get; set; }
        public TimeSpan TimeLimit { get => TimeSpan.FromMilliseconds(this.TimeLimitU); }
        [Obsolete("Raw Value, use GbxTimeLimitClass.AuthorScore instead", false)]
        public uint AuthorScoreU { get; set; }
        public TimeSpan AuthorScore { get => TimeSpan.FromMilliseconds(this.AuthorScoreU); }
    }

    public class GbxTimeLimitClassParser
        : ClassParser<GbxTimeLimitClass>
    {
        protected override int ChunkId => 0x0305B008;

        public override bool Skippable => true;

        protected override GbxTimeLimitClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxTimeLimitClass()
            {
                TimeLimitU = reader.ReadUInt32(),
                AuthorScoreU = reader.ReadUInt32()
            };
        }
    }
}
