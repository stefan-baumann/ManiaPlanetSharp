using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Various
{
    public class CollectionDescription
        : Node
    {
        public byte Version { get; set; }
        public string Collection { get; set; }
        public bool NeedsUnlock { get; set; }
        public string IconEnvironment { get; set; }
        public string IconCollection { get; set; }
        public int SortIndex { get; set; }
        public string DefaultZone { get; set; }
        public string Vehicle { get; set; }
        public string VehicleCollection { get; set; }
        public string VehicleAuthor { get; set; }
        public string MapFid { get; set; }
        public Vector2D Unused1 { get; set; }
        public Vector2D Unused2 { get; set; }
        public Vector2D MapCoordinateElement { get; set; }
        public Vector2D MapCoordinateIcon { get; set; }
        public string LoadScreen { get; set; }
        public Vector2D MapCoordinateDesc { get; set; }
        public string LongDescription { get; set; }
        public string DisplayName { get; set; }
        public bool IsEditable { get; set; }
    }

    public class CollectionDescriptionParser
        : ClassParser<CollectionDescription>
    {
        protected override int ChunkId => 0x03033001;

        protected override CollectionDescription ParseChunkInternal(GameBoxReader reader)
        {
            var result = new CollectionDescription();
            result.Version = reader.ReadByte();
            result.Collection = reader.ReadLookbackString();
            result.NeedsUnlock = reader.ReadBool();
            if (result.Version >= 1)
            {
                result.IconEnvironment = reader.ReadString();
                result.IconCollection = reader.ReadString();
                if (result.Version >= 2)
                {
                    result.SortIndex = reader.ReadInt32();
                    if (result.Version >= 3)
                    {
                        result.DefaultZone = reader.ReadLookbackString();
                        if (result.Version >= 4)
                        {
                            result.Vehicle = reader.ReadLookbackString();
                            result.VehicleCollection = reader.ReadLookbackString();
                            result.VehicleAuthor = reader.ReadLookbackString();
                            if (result.Version >= 5)
                            {
                                result.MapFid = reader.ReadString();
                                result.Unused1 = reader.ReadVec2D();
                                result.Unused2 = reader.ReadVec2D();
                                if (result.Version <= 7)
                                {
                                    result.MapCoordinateElement = reader.ReadVec2D();
                                    if (result.Version >= 6)
                                    {
                                        result.MapCoordinateIcon = reader.ReadVec2D();
                                    }
                                }
                                if (result.Version >= 7)
                                {
                                    result.LoadScreen = reader.ReadString();
                                    if (result.Version >= 8)
                                    {
                                        result.MapCoordinateElement = reader.ReadVec2D();
                                        result.MapCoordinateIcon = reader.ReadVec2D();
                                        result.MapCoordinateDesc = reader.ReadVec2D();
                                        result.LongDescription = reader.ReadString();
                                        if (result.Version >= 9)
                                        {
                                            result.DisplayName = reader.ReadString();
                                            if (result.Version >= 10)
                                            {
                                                result.IsEditable = reader.ReadBool();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
