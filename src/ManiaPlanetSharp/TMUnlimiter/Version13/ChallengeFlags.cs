namespace ManiaPlanetSharp.TMUnlimiter.Version13
{
    [System.Flags]
    enum ChallengeFlags : ushort
    {
        DecorationVisibility_SkyOnly = 1 << 0,
        IsDecorationMoved = 1 << 1,
        DecorationVisibility_Nothing = 1 << 2,
        IsDecorationScaled = 1 << 3,
        IsTrackBaseEmpty = 1 << 4,
        IsVanillaMode = 1 << 5,
        DecorationVisibility_Warp = 1 << 8,
        IsPylonsDisabled = 1 << 9,
        ReservedBit = 1 << 15,
    };
}