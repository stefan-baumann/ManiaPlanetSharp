namespace ManiaPlanetSharp.TMUnlimiter.Version06
{
    public class BlockData : TMUnlimiter.BlockData
    {
        public uint InternalOverOverSizeChunkX { get; set; }
        public uint InternalOverOverSizeChunkY { get; set; }
        public uint InternalOverOverSizeChunkZ { get; set; }
        public bool InternalIsClassicTerrain { get; set; }

        public BlockData( uint blockIndex ) : base( blockIndex ) {}

        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter06;
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
    }
}