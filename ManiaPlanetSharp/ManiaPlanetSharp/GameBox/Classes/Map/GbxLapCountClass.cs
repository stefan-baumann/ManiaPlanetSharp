using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxLapCountClass
        : GbxClass
    {
        public bool Unused { get; set; }
        public int LapCount { get; set; }
    }

    public class GbxLapCountClassParser
        : GbxClassParser<GbxLapCountClass>
    {
        protected override int ChunkId => 0x03043018;

        public override bool Skippable => true;

        protected override GbxLapCountClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxLapCountClass()
            {
                Unused = reader.ReadBool(),
                LapCount = (int)reader.ReadUInt32()
            };
        }
    }
}
