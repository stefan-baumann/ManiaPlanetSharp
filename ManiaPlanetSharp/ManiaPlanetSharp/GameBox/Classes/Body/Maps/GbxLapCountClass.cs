using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxLapCountClass
        : GbxBodyClass
    {
        public bool Unused { get; set; }
        public int LapCount { get; set; }
    }

    public class GbxLapCountClassParser
        : GbxBodyClassParser<GbxLapCountClass>
    {
        protected override int Chunk => 0x03043018;

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
