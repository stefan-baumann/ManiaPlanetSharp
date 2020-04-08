using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093000)]
    public class ReplayVersionChunk
        : Chunk
    {
        [Property]
        public uint Version { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string TrackUid { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string TrackEnvironment { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string TrackAuthor { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public uint TimeU { get; set; }
        public TimeSpan Time => TimeSpan.FromMilliseconds(this.TimeU);

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 2)]
        public string Nickname { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 6)]
        public string Login { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 8)]
        public byte Unused { get; set; }

        [Property(SpecialPropertyType.LookbackString), Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 8)]
        public string TitleUid { get; set; }
    }
}
