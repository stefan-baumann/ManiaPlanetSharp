using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ManiaPlanetSharp.Utilities;

namespace ManiaPlanetSharp.GameBox
{
    public /*abstract*/ class GbxClass
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

        public /*override*/ bool CanParse(uint chunkId)
        {
            return chunkId == this.ChunkId;
        }

        public /*override*/ TBodyClass ParseChunk(GbxNode chunk)
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

        public /*override*/ TBodyClass ParseChunk(GbxReader reader)
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
        private static IGbxClassParser<GbxClass>[] Parsers = new IGbxClassParser<GbxClass>[]
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
            new GbxBodyClassAutoParser<GbxDecoratorSolidClass>(0x2E00200A),
            new GbxBodyClassAutoParser<GbxStemMaterialClass>(0x2E00200B),
            new GbxBodyClassAutoParser<GbxRaceInterfaceClass>(0x2E00200C),
            new GbxBodyClassAutoParser<GbxBannerProfileClass>(0x2E002010),
            new GbxBodyClassAutoParser<GbxGroundPointClass>(0x2E002012),
            new GbxBodyClassAutoParser<GbxAudioEnvironmentClass>(0x2E002013),
            new GbxBodyClassAutoParser<GbxObjectBaseAttributeClass>(0x2E002014),
            new GbxBodyClassAutoParser<GbxObjectTypeInfoClass>(0x2E002015),
            new GbxBodyClassAutoParser<GbxDefaultSkinClass>(0x2E002016),
            new GbxBodyClassAutoParser<GbxFreelyAnchorableClass>(0x2E002017),
            new GbxBodyClassAutoParser<GbxObjectUsabilityClass>(0x2E002018),
            new GbxObjectModelClassParser(),
            //0x2E00201A
            //0x2E00201B
            //0x2E00201C
            //0x2E00201E
            //0x2E00201F
            //0x2E002020

            //Collector Files
            new GbxBodyClassAutoParser<GbxCollectorCatalogClass>(0x2E001007),
            new GbxCollectorBrowserMetadataClassParser(),
            new GbxBodyClassAutoParser<GbxCollectorBasicMetadataClass>(0x2E00100B),
            new GbxBodyClassAutoParser<GbxCollectorNameClass>(0x2E00100C),
            new GbxBodyClassAutoParser<GbxCollectorDescriptionClass>(0x2E00100D),
            new GbxBodyClassAutoParser<GbxCollectorIconMetadataClass>(0x2E00100E),
            new GbxBodyClassAutoParser<GbxCollectorDefaultSkinClass>(0x2E00100F),
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
            return GbxBodyClassParser.Parsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
