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
        [Obsolete("", false)]
        public uint KindU { get; set; }
        public GbxMapKind Kind { get => (GbxMapKind)(byte)this.KindU; }
    }

    public class GbxChallengeParameterClassParser
        : GbxBodyClassParser<GbxChallengeParameterClass>
    {
        protected override int Chunk => 0x03043011;

        protected override GbxChallengeParameterClass ParseChunkInternal(GbxReader reader)
        {
            GbxChallengeParameterClass challengeParams = new GbxChallengeParameterClass();
            //challengeParams.CollectorList = new GbxNodeParser().ParseNode(reader);
            //challengeParams.ChallengeParameters = new GbxNodeParser().ParseNode(reader);
            challengeParams.CollectorList = reader.ReadNodeReference();
            challengeParams.ChallengeParameters = reader.ReadNodeReference();
            challengeParams.KindU = reader.ReadUInt32();

            return challengeParams;
        }
    }
}
