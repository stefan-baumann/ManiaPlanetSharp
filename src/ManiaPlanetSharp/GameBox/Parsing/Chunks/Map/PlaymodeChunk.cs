using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 // Type or member is obsolete

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x0304301C, Skippable = true)]
    public class PlaymodeChunk
        : Chunk
    {
        [Property]
        [Obsolete("Raw Value, use GbxPlaymodeClass.Playmode instead", false)]
        public uint PlaymodeU { get; set; }
        public TrackType Playmode { get => (TrackType)this.PlaymodeU; }
    }

    public enum TrackType
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
}
