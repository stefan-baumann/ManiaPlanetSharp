using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092017, Skippable = true)]
    public class GhostDisplayChunk
        : Chunk
    {
        [Property, Array]
        public FileReference[] SkinPackDescriptors { get; set; }

        [Property]
        public string GhostNickname { get; set; }

        [Property]
        public string GhostAvatarName { get; set; }
    }
}
