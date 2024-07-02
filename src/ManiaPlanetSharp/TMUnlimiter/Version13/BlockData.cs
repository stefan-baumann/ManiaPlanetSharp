using ManiaPlanetSharp.GameBox;

namespace ManiaPlanetSharp.TMUnlimiter.Version13
{
    public class BlockData : TMUnlimiter.BlockData
    {
        public uint InternalOverOverSizeChunkX { get; set; }
        public uint InternalOverOverSizeChunkY { get; set; }
        public uint InternalOverOverSizeChunkZ { get; set; }
        public bool InternalIsClassicTerrain { get; set; }
        public bool InternalIsInverted { get; set; }
        public Vector3D InternalBlockOffset { get; set; } = new Vector3D();
        public Vector3D InternalBlockRotation { get; set; } = new Vector3D();
        public Vector3D InternalBlockScale { get; set; } = new Vector3D( 1.0f, 1.0f, 1.0f );
        public bool InternalIsSpawnPointFixEnabled { get; set; }
        public bool InternalIsDynamic { get; set; }
        public bool InternalIsInvisible { get; set; }
        public bool InternalIsCollisionDisabled { get; set; }
        public string InternalBlockGroup { get; set; }

        public BlockData( uint blockIndex ) : base( blockIndex ) {}

        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter13;
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
            return this.InternalBlockOffset.X != 0 || this.InternalBlockOffset.Y != 0 || this.InternalBlockOffset.Z != 0;
        }

        public override bool IsRotationApplied()
        {
            return this.InternalBlockRotation.X != 0 || this.InternalBlockRotation.Y != 0 || this.InternalBlockRotation.Z != 0;
        }

        public override bool IsScaleApplied()
        {
            return this.InternalBlockScale.X != 1 || this.InternalBlockScale.Y != 1 || this.InternalBlockScale.Z != 1;
        }

        public override Vector3D GetOffset()
        {
            return new Vector3D( this.InternalBlockOffset.X, this.InternalBlockOffset.Y, this.InternalBlockOffset.Z );
        }

        public override Vector3D GetRotation()
        {
            return new Vector3D( this.InternalBlockRotation.X, this.InternalBlockRotation.Y, this.InternalBlockRotation.Z );
        }

        public override Vector3D GetScale()
        {
            return new Vector3D( this.InternalBlockScale.X, this.InternalBlockScale.Y, this.InternalBlockScale.Z );
        }

        public override SpawnPointAlterMethod GetSpawnPointAlterMethod()
        {
            return this.InternalIsSpawnPointFixEnabled ? SpawnPointAlterMethod.Automatic : SpawnPointAlterMethod.None;
        }

        public override bool IsDynamic()
        {
            return this.InternalIsDynamic;
        }

        public override bool IsInvisible()
        {
            return this.InternalIsInvisible;
        }

        public override bool IsCollisionDisabled()
        {
            return this.InternalIsCollisionDisabled;
        }

        public override bool IsInBlockGroup( string blockGroup )
        {
            return this.InternalBlockGroup != null && this.InternalBlockGroup == blockGroup;
        }
    }
}