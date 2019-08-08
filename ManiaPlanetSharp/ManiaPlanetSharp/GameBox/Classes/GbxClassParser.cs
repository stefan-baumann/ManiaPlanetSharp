using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ManiaPlanetSharp.Utilities;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxClass
        : GbxNode
    {
        public GbxClass()
        { }
    }

    public abstract class GbxClassParser<TBodyClass>
        : IGbxClassParser<TBodyClass>
        where TBodyClass : GbxClass, new()
    {
        protected abstract int ChunkId { get; }

        public virtual bool Skippable { get; } = false;

        protected abstract TBodyClass ParseChunkInternal(GbxReader reader);

        public bool CanParse(uint chunkId)
        {
            return chunkId == this.ChunkId;
        }

        public TBodyClass ParseChunk(GbxNode chunk)
        {
            using (MemoryStream chunkStream = chunk.GetDataStream())
            using (GbxReader reader = new GbxReader(chunkStream))
            {
                return this.ParseChunkInternal(reader);
            }
        }

        public TBodyClass ParseChunk(GbxReader reader)
        {
            long startPosition = reader.Stream.Position;
            TBodyClass result = this.ParseChunkInternal(reader);
            long endPosition = reader.Stream.Position;
            reader.Stream.Position = startPosition;
            result.Data = reader.ReadRaw((int)(endPosition - startPosition));
            return result;
        }
    }

    public static class GbxClassParser
    {
        private static IGbxClassParser<GbxClass>[] Parsers = new IGbxClassParser<GbxClass>[]
        {
            new GbxVehicleClassParser(),
            new GbxChallengeParameterClassParser(),
            new GbxUnusedClassParser(0x03043012, reader => reader.ReadString()),
            //new GbxUnusedBodyClassParser(0x03043013, reader => throw new NotImplementedException("ReadChunk(0x0304301F)")),
            new GbxClassReferenceParser<GbxMapClass>(0x03043013, new GbxMapClassParser()),
            new GbxOldPasswordClassParser(),
            new GbxCheckpointsClassParser(),
            new GbxLapCountClassParser(),
            new GbxModpackClassParser(),
            new GbxPlaymodeClassParser(),
            new GbxMapClassParser(),
            new GbxMediaTrackerClassParser(),
            new GbxUnusedClassParser(0x03043022, reader => reader.ReadUInt32()),
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
            new GbxUnusedClassParser(0x0304302A, reader => reader.ReadBool()),
            new GbxUnusedClassParser(0x0305B000, reader => Utils.Repeat(reader.ReadUInt32, 8)),
            new GbxTipClassParser(),
            new GbxUnusedClassParser(0x0305B002, reader => {
                Utils.Repeat(reader.ReadUInt32, 3);
                Utils.Repeat(reader.ReadFloat, 3);
                Utils.Repeat(reader.ReadUInt32, 10);
            }),
            new GbxUnusedClassParser(0x0305B003, reader => {
                reader.ReadUInt32();
                reader.ReadFloat();
                Utils.Repeat(reader.ReadUInt32, 3);
                reader.ReadUInt32();
            }),
            new GbxTimeClassParser(),
            new GbxUnusedClassParser(0x0305B005, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            new GbxUnusedClassParser(0x0305B006, reader => { //Maybe move to separate class
                uint count = reader.ReadUInt32();
                Utils.Repeat(reader.ReadUInt32, (int)count);
            }),
            new GbxUnusedClassParser(0x0305B007, reader => reader.ReadUInt32()),
            new GbxTimeLimitClassParser(),
            new GbxTimeLimitTimeClassParser(),
            new GbxUnusedClassParser(0x0305B00D, reader => reader.ReadUInt32()),
            new GbxUnusedClassParser(0x0305B00E, true, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            //new GbxCollectorListClassParser(),
            new GbxWaypointSpecialPropertyClassParser(),
            //Maybe this is the right one for the job?
            new GbxClassReferenceParser<GbxWaypointSpecialPropertyClass>(0x2E009001, new GbxWaypointSpecialPropertyClassParser()),
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
            new GbxLightmapClassParser(),
            //0x03043044 (ManiaScript)
            //0x21080001 (Virtual Skipper metadata)
            new GbxBlockSkinClassParserA(),
            new GbxBlockSkinClassParserB(),
            new GbxBlockSkinClassParserC(),



            //Objects
            new GbxObjectCameraIndexClassParser(),
            new GbxNadeoSkinFidsClassParser(),
            new GbxObjectCameraClassParser(),
            new GbxClassAutoParser<GbxDecoratorSolidClass>(0x2E00200A),
            new GbxClassAutoParser<GbxStemMaterialClass>(0x2E00200B),
            new GbxClassAutoParser<GbxRaceInterfaceClass>(0x2E00200C),
            new GbxClassAutoParser<GbxBannerProfileClass>(0x2E002010),
            new GbxClassAutoParser<GbxGroundPointClass>(0x2E002012),
            new GbxClassAutoParser<GbxAudioEnvironmentClass>(0x2E002013),
            new GbxClassAutoParser<GbxObjectBaseAttributeClass>(0x2E002014),
            new GbxClassAutoParser<GbxObjectTypeInfoClass>(0x2E002015),
            new GbxClassAutoParser<GbxDefaultSkinClass>(0x2E002016),
            new GbxClassAutoParser<GbxFreelyAnchorableClass>(0x2E002017),
            new GbxClassAutoParser<GbxObjectUsabilityClass>(0x2E002018),
            new GbxObjectModelClassParser(),
            //0x2E00201A
            //0x2E00201B
            //0x2E00201C
            //0x2E00201E
            //0x2E00201F
            //0x2E002020

            //Collector Files
            new GbxClassAutoParser<GbxCollectorCatalogClass>(0x2E001007),
            new GbxCollectorBrowserMetadataClassParser(),
            new GbxClassAutoParser<GbxCollectorBasicMetadataClass>(0x2E00100B),
            new GbxClassAutoParser<GbxCollectorNameClass>(0x2E00100C),
            new GbxClassAutoParser<GbxCollectorDescriptionClass>(0x2E00100D),
            new GbxClassAutoParser<GbxCollectorIconMetadataClass>(0x2E00100E),
            new GbxClassAutoParser<GbxCollectorDefaultSkinClass>(0x2E00100F),
            //0x2E001010
            //0x2E001011

            //Replays
            new GbxReplayMapClassParser(),
            new GbxReplayBasicMetadataClassParser(),
            new GbxReplayCommunityClassParser(), //Implement XML parsing

            //Other
            //0x2E006001 (Physical Model?)
            //0x2E007001 (Visual Model?)
        };

        public static IGbxClassParser<GbxClass> GetParser(uint chunkId)
        {
            return GbxClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
