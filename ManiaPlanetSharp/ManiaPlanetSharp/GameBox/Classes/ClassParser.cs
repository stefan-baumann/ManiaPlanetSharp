using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ManiaPlanetSharp.GameBox.Classes.Collector;
using ManiaPlanetSharp.GameBox.Classes.Ghost;
using ManiaPlanetSharp.GameBox.Classes.Map;
using ManiaPlanetSharp.GameBox.Classes.Object;
using ManiaPlanetSharp.GameBox.Classes.Replay;
using ManiaPlanetSharp.Utilities;

namespace ManiaPlanetSharp.GameBox
{
    public interface IClassParser<out TClass>
        where TClass : class
    {
        bool CanParse(uint chunkId);

        bool Skippable { get; }

        TClass ParseChunk(GameBoxReader chunk);
    }

    public abstract class ClassParser<TClass>
        : IClassParser<TClass>
        where TClass : Node, new()
    {
        protected abstract int ChunkId { get; }

        public virtual bool Skippable { get; } = false;

        protected abstract TClass ParseChunkInternal(GameBoxReader reader);

        public bool CanParse(uint chunkId)
        {
            return chunkId == this.ChunkId;
        }

        public TClass ParseChunk(Node chunk)
        {
            using (MemoryStream chunkStream = chunk.GetDataStream())
            using (GameBoxReader reader = new GameBoxReader(chunkStream))
            {
                return this.ParseChunkInternal(reader);
            }
        }

        public TClass ParseChunk(GameBoxReader reader)
        {
            long startPosition = reader.Stream.Position;
            TClass result = this.ParseChunkInternal(reader);
            long endPosition = reader.Stream.Position;
            reader.Stream.Position = startPosition;
            result.Data = reader.ReadRaw((int)(endPosition - startPosition));
            return result;
        }
    }

    public static class ClassParser
    {
        private static IClassParser<Node>[] BodyClassParsers = new IClassParser<Node>[]
        {
            //Map
            new GbxVehicleClassParser(),
            new GbxChallengeParameterClassParser(),
            new UnusedClassParser(0x03043012, reader => reader.ReadString()),
            //new GbxUnusedBodyClassParser(0x03043013, reader => throw new NotImplementedException("ReadChunk(0x0304301F)")),
            new ClassReferenceParser<GbxMapClass>(0x03043013, new GbxMapClassParser()),
            new GbxOldPasswordClassParser(),
            new GbxCheckpointsClassParser(),
            new GbxLapCountClassParser(),
            new GbxModpackClassParser(),
            new GbxPlaymodeClassParser(),
            new GbxMapClassParser(),
            new GbxMediaTrackerClassParser(),
            new UnusedClassParser(0x03043022, reader => reader.ReadUInt32()),
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
            new UnusedClassParser(0x0304302A, reader => reader.ReadBool()),
            new UnusedClassParser(0x0305B000, reader => Utils.Repeat(reader.ReadUInt32, 8)),
            new GbxTipClassParser(),
            new UnusedClassParser(0x0305B002, reader => {
                Utils.Repeat(reader.ReadUInt32, 3);
                Utils.Repeat(reader.ReadFloat, 3);
                Utils.Repeat(reader.ReadUInt32, 10);
            }),
            new UnusedClassParser(0x0305B003, reader => {
                reader.ReadUInt32();
                reader.ReadFloat();
                Utils.Repeat(reader.ReadUInt32, 3);
                reader.ReadUInt32();
            }),
            new GbxTimeClassParser(),
            new UnusedClassParser(0x0305B005, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            new UnusedClassParser(0x0305B006, reader => { //Maybe move to separate class
                uint count = reader.ReadUInt32();
                Utils.Repeat(reader.ReadUInt32, (int)count);
            }),
            new UnusedClassParser(0x0305B007, reader => reader.ReadUInt32()),
            new GbxTimeLimitClassParser(),
            new GbxTimeLimitTimeClassParser(),
            new UnusedClassParser(0x0305B00D, reader => reader.ReadUInt32()),
            new UnusedClassParser(0x0305B00E, true, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            //new GbxCollectorListClassParser(),
            new GbxWaypointSpecialPropertyClassParser(),
            //Maybe this is the right one for the job?
            new ClassReferenceParser<GbxWaypointSpecialPropertyClass>(0x2E009001, new GbxWaypointSpecialPropertyClassParser()),
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
            //new GbxLightmapClassParser(), //Not working properly yet
            //0x03043044 (ManiaScript)
            //0x21080001 (Virtual Skipper metadata)
            new GbxBlockSkinClassParserA(),
            new GbxBlockSkinClassParserB(),
            new GbxBlockSkinClassParserC(),



            //Object
            new ObjectCameraIndexParser(),
            new ObjectNadeoSkinFilesParser(),
            new ObjectCamerassParser(),
            new AutoClassParser<ObjectDecoratorSolid>(0x2E00200A),
            new AutoClassParser<ObjectMaterial>(0x2E00200B),
            new AutoClassParser<ObjectRaceInterface>(0x2E00200C),
            new AutoClassParser<ObjectBannerProfile>(0x2E002010),
            new AutoClassParser<ObjectGroundPoint>(0x2E002012),
            new AutoClassParser<ObjectAudioEnvironment>(0x2E002013),
            new AutoClassParser<ObjectBaseAttributes>(0x2E002014),
            new AutoClassParser<ObjectTypeInfo>(0x2E002015),
            new AutoClassParser<ObjectDefaultSkin>(0x2E002016),
            new AutoClassParser<ObjectAnchor>(0x2E002017),
            new AutoClassParser<ObjectUsability>(0x2E002018),
            new ObjectModelParser(),
            //0x2E00201A
            //0x2E00201B
            //0x2E00201C
            //0x2E00201E
            //0x2E00201F
            //0x2E002020

            //Collector
            new AutoClassParser<GbxCollectorCatalogClass>(0x2E001007),
            new GbxCollectorBrowserMetadataClassParser(),
            new AutoClassParser<GbxCollectorBasicMetadataClass>(0x2E00100B),
            new AutoClassParser<GbxCollectorNameClass>(0x2E00100C),
            new AutoClassParser<GbxCollectorDescriptionClass>(0x2E00100D),
            new AutoClassParser<GbxCollectorIconMetadataClass>(0x2E00100E),
            new AutoClassParser<GbxCollectorDefaultSkinClass>(0x2E00100F),
            //0x2E001010
            //0x2E001011

            //Replay
            new ReplayEmbeddedMapParser(),
            new ReplayRecordParser(),
            new ReplayCommunityParser(), //Implement XML parsing
            new UnusedClassParser(0x03093007, true, reader => reader.ReadUInt32()),
            new AutoClassParser<ReplayGhosts>(0x03093014),
            new UnusedClassParser(0x03093015, reader => reader.ReadNodeReference()),

            //Ghost
            new GhostParserA(),
            new GhostParserB(),
            new AutoClassParser<GhostRaceTime>(0x03092005, true),
            new AutoClassParser<GhostRespawnCount>(0x03092008, true),
            new AutoClassParser<GhostLighttrail>(0x03092009, true),
            new AutoClassParser<GhostStuntScore>(0x0309200A, true),
            new UnusedClassParser(0x0309200B, true, reader => {
                uint count = reader.ReadUInt32();
                ulong[] values = new ulong[count];
                for (int i = 0; i < count; i++)
                {
                    values[i] = reader.ReadUInt64();
                }
            }),
            new UnusedClassParser(0x0309200C, reader => reader.ReadUInt32()),
            new AutoClassParser<GhostUid>(0x0309200E),
            new AutoClassParser<GhostLogin>(0x0309200F),
            new UnusedClassParser(0x03092010, reader => reader.ReadLookbackString()),
            new UnusedClassParser(0x03092012, reader => {
                uint a = reader.ReadUInt32();
                ulong[] b = reader.ReadUInt128();
            }),
            new UnusedClassParser(0x03092013, true, reader => Utils.Repeat(reader.ReadUInt32, 2)),
            new UnusedClassParser(0x03092014, true, reader => reader.ReadUInt32()),
            new AutoClassParser<GhostPlayerMobilId>(0x03092005, true),
            new AutoClassParser<GhostSkin>(0x03092017, true),
            new UnusedClassParser(0x03092018, reader => Utils.Repeat(reader.ReadLookbackString, 3)),

            //Other
            //0x2E006001 (Physical Model?)
            //0x2E007001 (Visual Model?)
        };

        public static IClassParser<Node> GetBodyClassParser(uint chunkId)
        {
            return ClassParser.BodyClassParsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
        
        private static IClassParser<Node>[] HeaderClassParsers = new IClassParser<Node>[]
        {
            //Map
            new GbxTmDescriptionClassParser(),
            new GbxCommonClassParser(),
            new GbxVersionClassParser(),
            new GbxMapCommunityClassParser(),
            new GbxThumbnailClassParser(),
            new GbxAuthorClassParser(),

            //Object
            new AutoClassParser<ObjectTypeInfo>(0x2E002000),
            new UnusedClassParser(0x2E002001, reader => reader.ReadUInt32()),

            //Collector
            new GbxCollectorMainDescriptionClassParser(),
            new GbxCollectorIconParser(),
            new GbxCollectorLightmapCacheIdParser(),

            //Replay
            new AutoClassParser<ReplayMapAuthor>(0x03093002),
        };

        public static IClassParser<Node> GetHeaderClassParser(uint chunkId)
        {
            return ClassParser.HeaderClassParsers.FirstOrDefault(parser => parser.CanParse(chunkId));
        }
    }
}
