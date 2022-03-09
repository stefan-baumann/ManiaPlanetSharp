using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x3043002)]
    public class MapDescriptionChunk
        : Chunk
    {
        [Property]
        public byte Version { get; set; }



        [Property, Condition(nameof(Version), ConditionOperator.LessThan, 3)]
        [Obsolete("Old field, not used anymore", false)]
        public string Uid { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.LessThan, 3)]
        [Obsolete("Old field, not used anymore", false)]
        public string Environment { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.LessThan, 3)]
        [Obsolete("Old field, not used anymore", false)]
        public string MapAuthor { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.LessThan, 3)]
        [Obsolete("Old field, not used anymore", false)]
        public string TrackName { get; set; }

        [Property]
        public bool Unknown1 { get; set; }



        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public uint BronzeTime { get; set; }

        public TimeSpan? BronzeTimeSpan { get => this.BronzeTime != uint.MaxValue ? (TimeSpan?)TimeSpan.FromMilliseconds(this.BronzeTime) : null; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public uint SilverTime { get; set; }

        public TimeSpan? SilverTimeSpan { get => this.SilverTime != uint.MaxValue ? (TimeSpan?)TimeSpan.FromMilliseconds(this.SilverTime) : null; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public uint GoldTime { get; set; }

        public TimeSpan? GoldTimeSpan { get => this.GoldTime != uint.MaxValue ? (TimeSpan?)TimeSpan.FromMilliseconds(this.GoldTime) : null; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 1)]
        public uint AuthorTime { get; set; }

        public TimeSpan? AuthorTimeSpan { get => this.AuthorTime != uint.MaxValue ? (TimeSpan?)TimeSpan.FromMilliseconds(this.AuthorTime) : null; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 2)]
        public byte Unknown2 { get; set; }



        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 4)]
        public uint Cost { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 5)]
        public bool Multilap { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.Equal, 6)]
        public byte Unknown3 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 7)]
        [Obsolete("Raw Value, use GbxTmDescriptionClass.TrackType instead", false)]
        public uint TrackTypeU { get; set; }

        public TrackType TrackType { get => (TrackType)this.TrackTypeU; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 9)]
        public uint Unknown4 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 10)]
        public uint AuthorScore { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 11)]
        [Obsolete("Raw Value, use MapDescriptionChunk.AdvancedEditor and MapDescriptionChunk.HasGhostblocks instead", false)]
        public uint EditorModeU { get; set; }

        public bool AdvancedEditor { get => (this.EditorModeU & 1) > 0; }

        public bool HasGhostBlocks { get => (this.EditorModeU & 2) > 0; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 12)]
        public bool Unknown5 { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 13)]
        public uint Checkpoints { get; set; }

        [Property, Condition(nameof(Version), ConditionOperator.GreaterThanOrEqual, 13)]
        public uint Laps { get; set; }
    }
}
