using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public enum ObjectType
        : uint
    {
        Undefined = 0,
        Ornament = 1,
        PickUp = 2,
        Character = 3,
        Vehicle = 4,
        Spot = 5,
        Cannon = 6,
        Group = 7,
        Decal = 8,
        Turret = 9,
        Wagon = 10,
        Block = 11,
        EntitySpawner = 12
    }

    public class ObjectTypeInfo
        : Node
    {
        [AutoParserProperty(0)]
        public uint ObjectTypeU { get; set; }
        public ObjectType ObjectType => (ObjectType)this.ObjectTypeU;
    }
}
