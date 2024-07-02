using ManiaPlanetSharp.GameBox;

namespace ManiaPlanetSharp.TMUnlimiter
{
    public abstract class BlockData
    {
        public uint BlockIndex { get; private set; }

        public BlockData( uint blockIndex )
        {
            this.BlockIndex = blockIndex;
        }

        public abstract TrackVersion GetTrackVersion();

        public virtual uint GetOverOverSizeChunkX()
        {
            return 0;
        }

        public virtual uint GetOverOverSizeChunkY()
        {
            return 0;
        }

        public virtual uint GetOverOverSizeChunkZ()
        {
            return 0;
        }

        public virtual bool IsClassicTerrain()
        {
            return false;
        }

        public virtual bool IsInverted()
        {
            return false;
        }

        public virtual bool IsOffsetApplied()
        {
            return false;
        }

        public virtual bool IsRotationApplied()
        {
            return false;
        }

        public virtual bool IsScaleApplied()
        {
            return false;
        }

        public virtual Vector3D GetOffset()
        {
            return new Vector3D();
        }

        public virtual Vector3D GetRotation()
        {
            return new Vector3D();
        }

        public virtual Vector3D GetScale()
        {
            return new Vector3D( 1.0f, 1.0f, 1.0f );
        }

        public virtual SpawnPointAlterMethod GetSpawnPointAlterMethod()
        {
            return SpawnPointAlterMethod.None;
        }

        public virtual bool IsDynamic()
        {
            return false;
        }

        public virtual bool IsInvisible()
        {
            return false;
        }

        public virtual bool IsCollisionDisabled()
        {
            return false;
        }

        public virtual bool IsInBlockGroup( string blockGroup )
        {
            return false;
        }
    }
}