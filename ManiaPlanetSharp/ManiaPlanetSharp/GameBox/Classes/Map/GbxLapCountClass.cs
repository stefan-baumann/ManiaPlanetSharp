using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxLapCountClass
        : Node
    {
        public bool Unused { get; set; }
        public int LapCount { get; set; }
    }

    public class GbxLapCountClassParser
        : ClassParser<GbxLapCountClass>
    {
        protected override int ChunkId => 0x03043018;

        public override bool Skippable => true;

        protected override GbxLapCountClass ParseChunkInternal(GameBoxReader reader)
        {
            return new GbxLapCountClass()
            {
                Unused = reader.ReadBool(),
                LapCount = (int)reader.ReadUInt32()
            };
        }
    }
}
