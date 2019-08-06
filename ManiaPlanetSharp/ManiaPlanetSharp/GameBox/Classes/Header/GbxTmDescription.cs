using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox
{
    public class GbxTmDescriptionClass
        : GbxHeaderClass
    {
        public byte Version { get; set; }

        //Map metadata, only used in version 1 and 2
        [Obsolete("Old field, not used anymore", false)]
        public string Uid { get; set; }
        [Obsolete("Old field, not used anymore", false)]
        public string Environment { get; set; }
        [Obsolete("Old field, not used anymore", false)]
        public string MapAuthor { get; set; }

        public uint BronzeTime { get; set; }
        public TimeSpan BronzeTimeSpan { get => TimeSpan.FromMilliseconds(this.BronzeTime); }
        public uint SilverTime { get; set; }
        public TimeSpan SilverTimeSpan { get => TimeSpan.FromMilliseconds(this.SilverTime); }
        public uint GoldTime { get; set; }
        public TimeSpan GoldTimeSpan { get => TimeSpan.FromMilliseconds(this.GoldTime); }
        public uint AuthorTime { get; set; }
        public TimeSpan AuthorTimeSpan { get => TimeSpan.FromMilliseconds(this.AuthorTime); }

        public uint Cost { get; set; }
        public bool Multilap { get; set; }
        [Obsolete("Raw Value, use GbxTmDescriptionClass.TrackType instead", false)]
        public uint TrackTypeU { get; set; }
        public GbxTrackType TrackType { get => (GbxTrackType)this.TrackTypeU; }
        public uint AuthorScore { get; set; }
        [Obsolete("Raw Value, use GbxTmDescriptionClass.AdvancedEditor and GbxTmDescriptionClass.HasGhostblocks instead", false)]
        public uint EditorMode { get; set; }
        public bool AdvancedEditor { get => (this.EditorMode & 1) > 0; }
        public bool HasGhostBlocks { get => (this.EditorMode & 2) > 0; }
        public uint Checkpoints { get; set; }
        public uint Laps { get; set; }
    }

    public enum GbxTrackType
        : uint
    {
        Race = 0,
        Platform = 1,
        Puzzle = 2,
        Crazy = 3,
        Shortcut = 4,
        Stunts = 5,
        Script = 6
    }

    public class GbxTmDescriptionClassParser
        : GbxHeaderClassParser<GbxTmDescriptionClass>
    {
        protected override int Chunk => 2;

        public override GbxTmDescriptionClass ParseChunk(GbxReader reader)
        {
            GbxTmDescriptionClass description = new GbxTmDescriptionClass();
            description.Version = reader.ReadByte();

            if (description.Version < 3)
            {
                description.Uid = reader.ReadLoopbackString();
                description.Environment = reader.ReadLoopbackString();
                description.MapAuthor = reader.ReadLoopbackString();
            }

            if (description.Version >= 1)
            {
                description.BronzeTime = reader.ReadUInt32();
                description.SilverTime = reader.ReadUInt32();
                description.GoldTime = reader.ReadUInt32();
                description.AuthorTime = reader.ReadUInt32();

                if (description.Version == 2) reader.ReadByte();

                if (description.Version >= 4)
                {
                    description.Cost = reader.ReadUInt32();
                    if (description.Version >= 5)
                    {
                        description.Multilap = reader.ReadBool();
                        if (description.Version == 6) reader.ReadBool();
                        if (description.Version >= 7)
                        {
                            description.TrackTypeU = reader.ReadUInt32();
                            if (description.Version >= 9)
                            {
                                reader.ReadUInt32();
                                if (description.Version >= 10)
                                {
                                    description.AuthorScore = reader.ReadUInt32();
                                    if (description.Version >= 11)
                                    {
                                        description.EditorMode = reader.ReadUInt32();
                                        if (description.Version >= 12)
                                        {
                                            reader.ReadBool();
                                            if (description.Version >= 13)
                                            {
                                                //Different order compared to the ManiaTechWiki entry
                                                description.Laps = reader.ReadUInt32();
                                                description.Checkpoints = reader.ReadUInt32();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return description;
        }
    }
}
