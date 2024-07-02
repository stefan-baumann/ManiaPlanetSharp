namespace ManiaPlanetSharp.TMUnlimiter.Version13
{
    [System.Flags]
    enum BlockFlags : ushort
    {
        IsOutsideBoundaries = 1 << 0,
        IsMoved = 1 << 1,
        IsRotated = 1 << 2,
        IsScaled = 1 << 3,
        IsInverted = 1 << 4,
        IsVanillaTerrain = 1 << 5,
        IsSpawnPointFixEnabled = 1 << 6,
        IsDynamic = 1 << 7,
        IsInvisible = 1 << 8,
        IsCollisionDisabled = 1 << 9,
        IsClassicMode = 1 << 10,
        IsClassicTerrain = 1 << 11,
        HasIdentifier = 1 << 14,
        Reserved = 1 << 15,
    }
}