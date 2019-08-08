using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 //Disable obsoleteness-warnings

namespace ManiaPlanetSharp.GameBox
{
    public class GbxCommonClass
        : GbxClass
    {
        public byte Version { get; set; }
        public string MapUid { get; set; }
        public string MapEnvironment { get; set; }
        public string MapAuthor { get; set; }
        public string MapName { get; set; }
        [Obsolete("Raw Value, use GbxCommonClass.Kind instead", false)]
        public byte KindB { get; set; }
        public GbxMapKind Kind { get => (GbxMapKind)this.KindB; }
        public bool Locked { get; set; }
        [Obsolete("Legacy value, use GbxPasswordClass.Password instead", false)]
        public string Password { get; set; }
        public string DecorationTimeOfDay { get; set; }
        public string DecorationEnvironment { get; set; }
        public string DecorationEnvironmentAuthor { get; set; }
        public GbxVec2D MapOrigin { get; set; }
        public GbxVec2D MapTarget { get; set; }
        public string MapType { get; set; }
        public string MapStyle { get; set; }
        public ulong LightmapCacheUid { get; set; }
        public byte LightmapVersion { get; set; }
        public string TitleUid { get; set; }
    }

    public enum GbxMapKind
        : byte
    {
        EndMarker = 0,
        [Obsolete("", false)]
        CampaignOld = 1,
        [Obsolete("", false)]
        Puzzle = 2,
        [Obsolete("", false)]
        Retro = 3,
        [Obsolete("", false)]
        TimeAttack = 4,
        [Obsolete("", false)]
        Rounds = 5,
        InProgress = 6,
        Campaign = 7,
        Multi = 8,
        Solo = 9,
        Site = 10,
        SoloNadeo = 11,
        MultiNadeo = 12
    }

    public class GbxCommonClassParser
        : GbxClassParser<GbxCommonClass>
    {
        protected override int ChunkId => 0x3043003;

        protected override GbxCommonClass ParseChunkInternal(GbxReader reader)
        {
            GbxCommonClass common = new GbxCommonClass();
            common.Version = reader.ReadByte();
            common.MapUid = reader.ReadLookbackString();
            common.MapEnvironment = reader.ReadLookbackString();
            common.MapAuthor = reader.ReadLookbackString();
            common.MapName = reader.ReadString();
            common.KindB = reader.ReadByte();
            if (common.Version >= 1)
            {
                common.Locked = reader.ReadBool();
                common.Password = reader.ReadString();
                if (common.Version >= 2)
                {
                    common.DecorationTimeOfDay = reader.ReadLookbackString();
                    common.DecorationEnvironment = reader.ReadLookbackString();
                    common.DecorationEnvironmentAuthor = reader.ReadLookbackString();
                    if (common.Version >= 3)
                    {
                        common.MapOrigin = reader.ReadVec2D();
                        if (common.Version >= 4)
                        {
                            common.MapTarget = reader.ReadVec2D();
                            if (common.Version >= 5)
                            {
                                ulong[] unused = reader.ReadUInt128();
                                if (common.Version >= 6)
                                {
                                    common.MapType = reader.ReadString();
                                    common.MapStyle = reader.ReadString();

                                    if (common.Version <= 8) reader.ReadBool();
                                    if (common.Version >= 8)
                                    {
                                        common.LightmapCacheUid = reader.ReadUInt64();
                                        if (common.Version >= 9)
                                        {
                                            common.LightmapVersion = reader.ReadByte();
                                            if (common.Version >= 11)
                                            {
                                                common.TitleUid = reader.ReadLookbackString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return common;
        }
    }
}
