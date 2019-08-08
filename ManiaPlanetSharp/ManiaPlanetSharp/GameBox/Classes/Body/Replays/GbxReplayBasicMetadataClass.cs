using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxReplayBasicMetadataClass
        : GbxClass
    {
        public int Version { get; set; }
        public string MapUid { get; set; }
        public string MapEnvironment { get; set; }
        public string MapAuthor { get; set; }
        public uint TimeU { get; set; }
        public TimeSpan Time { get => TimeSpan.FromMilliseconds(this.TimeU); }
        public string DriverNickName { get; set; }
        public string DriverLogin { get; set; }
        public byte Unused { get; set; }
        public string TitleUid { get; set; }
    }

    public class GbxReplayBasicMetadataClassParser
        : GbxClassParser<GbxReplayBasicMetadataClass>
    {
        protected override int ChunkId => 0x03093000;

        protected override GbxReplayBasicMetadataClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxReplayBasicMetadataClass();
            result.Version = (int)reader.ReadUInt32();
            if (result.Version >= 2)
            {
                result.MapUid = reader.ReadLookbackString();
                result.MapEnvironment = reader.ReadLookbackString();
                result.MapAuthor = reader.ReadLookbackString();
                result.TimeU = reader.ReadUInt32();
                result.DriverNickName = reader.ReadString();
                if (result.Version >= 6)
                {
                    result.DriverLogin = reader.ReadString();
                    if (result.Version >= 8)
                    {
                        result.Unused = reader.ReadByte();
                        result.TitleUid = reader.ReadLookbackString();
                    }
                }
            }

            return result;
        }
    }
}
