using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox.Classes.Map
{
    public class GbxChallengeParameterClass
        : Node
    {
        public Node CollectorList { get; set; }
        public Node ChallengeParameters { get; set; }
        [Obsolete("Raw Value, use GbxChallengeParameterClass.Kind instead", false)]
        public uint KindU { get; set; }
        public GbxMapKind Kind { get => (GbxMapKind)(byte)this.KindU; }
    }

    public class GbxChallengeParameterClassParser
        : ClassParser<GbxChallengeParameterClass>
    {
        protected override int ChunkId => 0x03043011;

        protected override GbxChallengeParameterClass ParseChunkInternal(GameBoxReader reader)
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
