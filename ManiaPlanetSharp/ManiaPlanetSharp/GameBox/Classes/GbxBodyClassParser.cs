using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ManiaPlanetSharp.Utilities;

namespace ManiaPlanetSharp.GameBox
{
    public /*abstract*/ class GbxBodyClass
        : GbxChunk
    {
        public GbxBodyClass()
            : base(0, 0, new byte[0])
        { }
    }

    public abstract class GbxBodyClassParser<TChallengeClass>
        : IGbxBodyClassParser<TChallengeClass>
        where TChallengeClass : GbxBodyClass, new()
    {
        protected abstract int Chunk { get; }

        public virtual bool Skippable { get; } = false;

        protected abstract TChallengeClass ParseChunkInternal(GbxReader reader);

        public /*override*/ bool CanParse(int chunkId)
        {
            return chunkId == this.Chunk;
        }

        public /*override*/ TChallengeClass ParseChunk(GbxChunk chunk)
        {
            using (MemoryStream chunkStream = chunk.GetDataStream())
            using (GbxReader reader = new GbxReader(chunkStream))
            {
                //try
                //{
                    return this.ParseChunkInternal(reader);
                //}
                //catch
                //{
                //    return new TChallengeClass();
                //}
            }
        }

        public /*override*/ TChallengeClass ParseChunk(GbxReader reader)
        {
            //try
            //{
              return this.ParseChunkInternal(reader);
            //}
            //catch (Exception ex)
            //{
            //    return new TChallengeClass();
            //}
        }
    }

    public static class GbxBodyClassParser
    {
        private static IGbxBodyClassParser<GbxBodyClass>[] Parsers = new IGbxBodyClassParser<GbxBodyClass>[]
        {
            new GbxVehicleClassParser(),
            new GbxChallengeParameterClassParser(),
            new GbxUnusedBodyClassParser(0x03043012, reader => reader.ReadString()),
            //new GbxUnusedBodyClassParser(0x03043013, reader => throw new NotImplementedException("ReadChunk(0x0304301F)")),
            new GbxBodyClassReferenceParser<GbxMapClass>(0x03043013, new GbxMapClassParser()),
            new GbxOldPasswordClassParser(),
            new GbxCheckpointsClassParser(),
            new GbxLapCountClassParser(),
            new GbxModpackClassParser(),
            new GbxPlaymodeClassParser(),
            new GbxMapClassParser(),
            new GbxMediaTrackerClassParser(),
            new GbxUnusedBodyClassParser(0x03043022, reader => reader.ReadUInt32()),
            new GbxCustomMusicClassParser(),
            new GbxMapCoordinateClassParser(),
            new GbxGlobalClipClassParser(),
            new GbxGmCamArchiveClassParser(),
            //new GbxUnusedBodyClassParser(0x03043028, reader => { //Commented out for testing purposes
            //    //Use for something
            //    Parsers[0x03043027].ParseChunk(reader);
            //    string comment = reader.ReadString();
            //}),
            new GbxPasswordClassParser(),
            new GbxUnusedBodyClassParser(0x0304302A, reader => reader.ReadBool()),
            new GbxUnusedBodyClassParser(0x0305B000, reader => Utils.Repeat(reader.ReadUInt32, 8)),
            new GbxTipClassParser(),
            new GbxUnusedBodyClassParser(0x0305B002, reader => {
                Utils.Repeat(reader.ReadUInt32, 3);
                Utils.Repeat(reader.ReadFloat, 3);
                Utils.Repeat(reader.ReadUInt32, 10);
            }),
            new GbxUnusedBodyClassParser(0x0305B003, reader => {
                reader.ReadUInt32();
                reader.ReadFloat();
                Utils.Repeat(reader.ReadUInt32, 3);
                reader.ReadUInt32();
            }),
            new GbxTimeClassParser(),
            new GbxUnusedBodyClassParser(0x0305B005, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            new GbxUnusedBodyClassParser(0x0305B006, reader => { //Maybe move to separate class
                uint count = reader.ReadUInt32();
                Utils.Repeat(reader.ReadUInt32, (int)count);
            }),
            new GbxUnusedBodyClassParser(0x0305B007, reader => reader.ReadUInt32()),
            new GbxTimeLimitClassParser(),
            new GbxTimeLimitTimeClassParser(),
            new GbxUnusedBodyClassParser(0x0305B00D, reader => reader.ReadUInt32()),
            new GbxUnusedBodyClassParser(0x0305B00E, true, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            //new GbxCollectorListClassParser(),
            new GbxWaypointSpecialPropertyClassParser(),
            //Maybe this is the right one for the job?
            new GbxBodyClassReferenceParser<GbxWaypointSpecialPropertyClass>(0x2E009001, new GbxWaypointSpecialPropertyClassParser()),
            new GbxPackDescriptorClassParser(),
           new GbxEmbeddedItemsClassParser(),
            //new GbxUnusedBodyClassParser(0x03043048, reader =>{
            //    throw new NotImplementedException();
            //}),
            //new GbxUnusedBodyClassParser(0x03043049, reader =>{
            //    throw new NotImplementedException();
            //}),

            //new GbxUnusedBodyClassParser(0x, true, reader => ),
            //new GbxUnusedBodyClassParser(0x, true, reader => {
            //
            //}),
        };

        public static IGbxBodyClassParser<GbxBodyClass> GetParser(int chunkId)
        {
            return GbxBodyClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
