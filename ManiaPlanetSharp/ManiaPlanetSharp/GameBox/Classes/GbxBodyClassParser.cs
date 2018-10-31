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
            new GbxUnusedBodyClassParser(0x03043014, true, reader => {
                reader.ReadUInt32();
                reader.ReadString();
            }),
            new GbxCheckpointsClassParser(),
            new GbxUnusedBodyClassParser(0x03043018, true, reader => {
                reader.ReadBool();
                uint lapCount = reader.ReadUInt32();
            }),
            new GbxUnusedBodyClassParser(0x03043019, true, reader => {
                GbxFileReference modPackDescriptor = reader.ReadFileRef();
            }),
            new GbxUnusedBodyClassParser(0x0304301C, true, reader => {
                uint playMode = reader.ReadUInt32();
            }),
            new GbxMapClassParser(),
            new GbxUnusedBodyClassParser(0x03043021, reader => {
                GbxNode clipIntro = reader.ReadNodeReference();
                GbxNode clipGroupInGame = reader.ReadNodeReference();
                GbxNode clipGrpupEndRace = reader.ReadNodeReference();
            }),
            new GbxUnusedBodyClassParser(0x03043022, reader => reader.ReadUInt32()),
            new GbxUnusedBodyClassParser(0x03043024, reader => {
                GbxFileReference customMusicPackDescriptor = reader.ReadFileRef();
            }),
            new GbxUnusedBodyClassParser(0x03043025, reader => {
                Vec2D mapCoordinateOrigin = reader.ReadVec2D();
                Vec2D mapCoordinateTarget = reader.ReadVec2D();
            }),
            new GbxUnusedBodyClassParser(0x03043026, reader => {
                GbxNode clipGlobal = reader.ReadNodeReference();
            }),
            new GbxUnusedBodyClassParser(0x03043027, reader => {
                bool archiveGmCamVal = reader.ReadBool();
                if (archiveGmCamVal)
                {
                    reader.ReadByte();
                    Utils.Repeat(reader.ReadVec3D, 3);

                    reader.ReadVec3D();
                    Utils.Repeat(reader.ReadFloat, 3);
                }
            }),
            new GbxUnusedBodyClassParser(0x03043028, reader => {
                //Use for something
                new GbxMapClassParser().ParseChunk(reader);
                string comment = reader.ReadString();
            }),
            new GbxBodyClassReferenceParser<GbxMapClass>(0x03043028, new GbxMapClassParser()),
            new GbxUnusedBodyClassParser(0x03043029, true, reader => {
                ulong[] passwordHash = reader.ReadUInt128();
                uint crc = reader.ReadUInt32();
            }),
            new GbxUnusedBodyClassParser(0x0304302A, reader => reader.ReadBool()),
            new GbxUnusedBodyClassParser(0x0305B000, reader => Utils.Repeat(reader.ReadUInt32, 8)),
            new GbxUnusedBodyClassParser(0x0305B001, reader => Utils.Repeat(reader.ReadString, 4)),
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
            new GbxUnusedBodyClassParser(0x0305B004, reader => {
                uint bronzeTime = reader.ReadUInt32();
                uint silverTime = reader.ReadUInt32();
                uint goldTime = reader.ReadUInt32();
                uint authorTime = reader.ReadUInt32();
                reader.ReadUInt32();
            }),
            new GbxUnusedBodyClassParser(0x0305B005, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            new GbxUnusedBodyClassParser(0x0305B006, reader => {
                uint count = reader.ReadUInt32();
                Utils.Repeat(reader.ReadUInt32, (int)count);
            }),
            new GbxUnusedBodyClassParser(0x0305B007, reader => reader.ReadUInt32()),
            new GbxUnusedBodyClassParser(0x0305B008, reader => {
                uint timeLimit = reader.ReadUInt32();
                uint authorScore = reader.ReadUInt32();
            }),
            new GbxUnusedBodyClassParser(0x0305B00A, true, reader => {
                reader.ReadUInt32();
                uint bronzeTime = reader.ReadUInt32();
                uint silverTime = reader.ReadUInt32();
                uint goldTime = reader.ReadUInt32();
                uint authorTime = reader.ReadUInt32();
                uint timeLimit = reader.ReadUInt32();
                uint authorScore = reader.ReadUInt32();
            }),
            new GbxUnusedBodyClassParser(0x0305B00D, reader => reader.ReadUInt32()),
            new GbxUnusedBodyClassParser(0x0305B00E, true, reader => Utils.Repeat(reader.ReadUInt32, 3)),
            //new GbxCollectorListClassParser(),
            new GbxWaypointSpecialPropertyClassParser(),
            //Maybe this is the right one for the job?
            new GbxBodyClassReferenceParser<GbxWaypointSpecialPropertyClass>(0x2E009001, new GbxWaypointSpecialPropertyClassParser()),
            new GbxUnusedBodyClassParser(0x03059002, reader => {
                string text = reader.ReadString();
                GbxFileReference packDescriptor = reader.ReadFileRef();
                GbxFileReference parentPackDescriptor = reader.ReadFileRef();
            }),
            new GbxUnusedBodyClassParser(0x03043054, true, reader => {
                uint version = reader.ReadUInt32();
                reader.ReadUInt32();
                uint chunkSize = reader.ReadUInt32();
                uint itemCount = reader.ReadUInt32();
                uint zipSize = reader.ReadUInt32();
                byte[] zipFile = reader.ReadRaw((int)zipSize);
            }),
            new GbxUnusedBodyClassParser(0x03043048, reader =>{
                throw new NotImplementedException();
            }),
            new GbxUnusedBodyClassParser(0x03043049, reader =>{
                throw new NotImplementedException();
            }),

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
