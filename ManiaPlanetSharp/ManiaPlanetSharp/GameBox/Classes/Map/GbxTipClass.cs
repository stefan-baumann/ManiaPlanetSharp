using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxTipClass
        : Node
    {
        public string Tip1 { get; set; }
        public string Tip2 { get; set; }
        public string Tip3 { get; set; }
        public string Tip4 { get; set; }
    }

    public class GbxTipClassParser
        : ClassParser<GbxTipClass>
    {
        protected override int ChunkId => 0x0305B001;

        protected override GbxTipClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxTipClass()
            {
                Tip1 = reader.ReadString(),
                Tip2 = reader.ReadString(),
                Tip3 = reader.ReadString(),
                Tip4 = reader.ReadString(),
            };
        }
    }
}
