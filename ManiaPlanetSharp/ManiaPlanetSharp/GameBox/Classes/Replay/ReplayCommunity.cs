using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Replay
{
    public class ReplayCommunity
        : Node
    {
        public string XmlString { get; set; }
    }

    public class ReplayCommunityParser
        : ClassParser<ReplayCommunity>
    {
        protected override int ChunkId => 0x03093001;

        protected override ReplayCommunity ParseChunkInternal(GameBoxReader reader)
        {
            return new ReplayCommunity()
            {
                XmlString = reader.ReadString()
            };
        }
    }
}
