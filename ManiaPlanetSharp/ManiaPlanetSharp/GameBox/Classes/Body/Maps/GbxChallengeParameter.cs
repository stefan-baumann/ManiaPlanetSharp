using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox
{
    public class GbxChallengeParameterClass
        : GbxBodyClass
    {
        public GbxNode CollectorList { get; set; }
        public GbxNode ChallengeParameters { get; set; }
        [Obsolete("Raw Value, use GbxChallengeParameterClass.Kind instead", false)]
        public uint KindU { get; set; }
        public GbxMapKind Kind { get => (GbxMapKind)(byte)this.KindU; }
    }

    public class GbxChallengeParameterClassParser
        : GbxBodyClassParser<GbxChallengeParameterClass>
    {
        protected override int Chunk => 0x03043011;

        protected override GbxChallengeParameterClass ParseChunkInternal(GbxReader reader)
        {
            return new GbxChallengeParameterClass()
            {
                CollectorList = reader.ReadNodeReference(),
                ChallengeParameters = reader.ReadNodeReference(),
                KindU = reader.ReadUInt32()
            };
        }
    }
}
