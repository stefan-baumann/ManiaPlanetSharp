using ManiaPlanetSharp.GameBox;

namespace ManiaPlanetSharp.TMUnlimiter.Version11
{
    public class BlockData : TMUnlimiter.BlockData
    {
        public uint InternalOverOverSizeChunkX { get; set; }
        public uint InternalOverOverSizeChunkY { get; set; }
        public uint InternalOverOverSizeChunkZ { get; set; }
        public bool InternalIsClassicTerrain { get; set; }
        public bool InternalIsInverted { get; set; }
        public int InternalBlockOffsetX { get; set; }
        public int InternalBlockOffsetY { get; set; }
        public int InternalBlockOffsetZ { get; set; }
        public uint InternalBlockRotationX { get; set; }
        public uint InternalBlockRotationY { get; set; }
        public uint InternalBlockRotationZ { get; set; }

        public BlockData( uint blockIndex ) : base( blockIndex ) {}

        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter11;
        }

        public override uint GetOverOverSizeChunkX()
        {
            return this.InternalOverOverSizeChunkX;
        }

        public override uint GetOverOverSizeChunkY()
        {
            return this.InternalOverOverSizeChunkY;
        }

        public override uint GetOverOverSizeChunkZ()
        {
            return this.InternalOverOverSizeChunkZ;
        }

        public override bool IsClassicTerrain()
        {
            return this.InternalIsClassicTerrain;
        }

        public override bool IsInverted()
        {
            return this.InternalIsInverted;
        }

        public override bool IsOffsetApplied()
        {
            return this.InternalBlockOffsetX != 0 || this.InternalBlockOffsetY != 0 || this.InternalBlockOffsetZ != 0;
        }

        public override bool IsRotationApplied()
        {
            return this.InternalBlockRotationX != 0 || this.InternalBlockRotationY != 0 || this.InternalBlockRotationZ != 0;
        }

        public override Vector3D GetOffset()
        {
            return new Vector3D( this.InternalBlockOffsetX, this.InternalBlockOffsetY, this.InternalBlockOffsetZ );
        }

        public override Vector3D GetRotation()
        {
            return new Vector3D( this.InternalBlockRotationX * 5, this.InternalBlockRotationY * 5, this.InternalBlockRotationZ * 5 );
        }
    }
}