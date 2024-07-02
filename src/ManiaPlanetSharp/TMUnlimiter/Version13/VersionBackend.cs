using System.Collections.Generic;
using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;

namespace ManiaPlanetSharp.TMUnlimiter.Version13
{
    public class VersionBackend : TMUnlimiter.VersionBackend
    {
        public static readonly List<ParameterFunction> VehicleParameterFunctions = new List<ParameterFunction>();
        public static readonly List<ParameterFunction> ResetParameterFunctions = new List<ParameterFunction>();
        public static readonly List<ParameterFunction> WorldParameterFunctions = new List<ParameterFunction>();
        public static readonly List<ParameterFunction> BlockParameterFunctions = new List<ParameterFunction>();
        public static readonly List<ParameterFunction> VehicleMultipliersParameterFunctions = new List<ParameterFunction>();

        static VersionBackend()
        {
            VehicleParameterFunctions.Add( null ); // "None" function
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Gravity", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Gravity (Total)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Simple Gravity Inversion", ParameterValueType.None ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Max Speed (Forward)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Max Speed (Backward)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Yellow Booster Strength", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Red Booster Strength", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Acceleration (Add)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Acceleration Multiplier", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Yellow Booster Duration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Red Boost Duration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Drift Deceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Air Deceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Steering Strength", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Steepness Acceleration", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Body Friction", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Vehicle Grip", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Idleness Properties", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Braking Properties", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Water Bounce Strength", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Air Stability", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Scale Vehicle", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( null ); // "Vehicle/Change Engine Pitch" - never implemented in 1.3
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Change Engine Volume", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Add Force (Axis X)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Add Force (Axis Y)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Add Force (Axis Z)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Set Gravity (Axis X)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Set Gravity (Axis Y)", ParameterValueType.Number ) );
            VehicleParameterFunctions.Add( new ParameterFunction( "Vehicle/Set Gravity (Axis Z)", ParameterValueType.Number ) );

            ResetParameterFunctions.Add( null ); // "None" function
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Everything", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Gravity", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Max Speed", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Forward Max Speed", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Backward Max Speed", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Booster Strength", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Yellow Booster Strength", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Red Booster Strength", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Acceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Booster Duration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Yellow Booster Duration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Red Booster Duration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Drift Deceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Air Deceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Steering Strength", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Steepness Acceleration", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Body Friction", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Vehicle Grip", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Idleness Properties", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Braking Properties", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Water Bounce", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Air Stability", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Engine Sound", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Engine Scale", ParameterValueType.None ) );
            ResetParameterFunctions.Add( new ParameterFunction( "Reset/Reset Gravity Force", ParameterValueType.None ) );

            WorldParameterFunctions.Add( null ); // "None" function
            WorldParameterFunctions.Add( new ParameterFunction( "World/Call Clip", ParameterValueType.Number ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Call And Display Clip", ParameterValueType.Number ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Add Script", ParameterValueType.Number ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Remove Script", ParameterValueType.Number ) );
            WorldParameterFunctions.Add( null ); // Not used space for a function
            WorldParameterFunctions.Add( null ); // Not used space for a function
            WorldParameterFunctions.Add( new ParameterFunction( "World/Set Mood Time", ParameterValueType.Number ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Enable Dynamic Mood", ParameterValueType.None ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Disable Dynamic Mood", ParameterValueType.None ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Set Dynamic Mood Speed", ParameterValueType.Number ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Enable Magnets", ParameterValueType.None ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Disable Magnets", ParameterValueType.None ) );
            WorldParameterFunctions.Add( new ParameterFunction( "World/Toggle Hard Magnets", ParameterValueType.None ) );

            BlockParameterFunctions.Add( null ); // "None" function
            BlockParameterFunctions.Add( new ParameterFunction( "Block/Show Block", ParameterValueType.String ) );
            BlockParameterFunctions.Add( new ParameterFunction( "Block/Hide Block", ParameterValueType.String ) );
            BlockParameterFunctions.Add( new ParameterFunction( "Block/Disable Block Collision", ParameterValueType.String ) );
            BlockParameterFunctions.Add( new ParameterFunction( "Block/Enable Block Collision", ParameterValueType.String ) );
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function
            BlockParameterFunctions.Add( null ); // Not used space for a function

            VehicleMultipliersParameterFunctions.Add( null ); // "None" function
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Gravity", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Max Speed (Forward)", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Max Speed (Backward)", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Yellow Booster Strength", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Red Booster Strength", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Acceleration Multipiler", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Yellow Booster Duration", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Red Booster Duration", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Drift Deceleration", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Air Deceleration", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Steering Strength", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Steepness Acceleration", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Body Friction", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Vehicle Grip", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Free Wheeling Brake", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Vehicle Brake", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Water Bounce Strength", ParameterValueType.Number ) );
            VehicleMultipliersParameterFunctions.Add( new ParameterFunction( "Vehicle (Multipliers)/Change Air Stability", ParameterValueType.Number ) );
        }

        public static ParameterFunction GetParameterFunction( uint catalogIndex, uint functionIndex )
        {
            switch ( catalogIndex )
            {
                case 0: // "Vehicle" catalog
                {
                    return
                    (
                        functionIndex >= VehicleParameterFunctions.Count
                        ?
                        null
                        :
                        VehicleParameterFunctions[ ( int )functionIndex ]
                    );
                }
                case 1: // "Reset" catalog
                {
                    return
                    (
                        functionIndex >= ResetParameterFunctions.Count
                        ?
                        null
                        :
                        ResetParameterFunctions[ ( int )functionIndex ]
                    );
                }
                case 2: // "World" catalog
                {
                    return
                    (
                        functionIndex >= WorldParameterFunctions.Count
                        ?
                        null
                        :
                        WorldParameterFunctions[ ( int )functionIndex ]
                    );
                }
                case 3: // "Block" catalog
                {
                    return
                    (
                        functionIndex >= BlockParameterFunctions.Count
                        ?
                        null
                        :
                        BlockParameterFunctions[ ( int )functionIndex ]
                    );
                }
                case 4: // "Vehicle (Multipliers)" catalog
                {
                    return
                    (
                        functionIndex >= VehicleMultipliersParameterFunctions.Count
                        ?
                        null
                        :
                        VehicleMultipliersParameterFunctions[ ( int )functionIndex ]
                    );
                }
                default: // Unknown catalog
                {
                    return null;
                }
            }
        }

        ChallengeFlags ChallengeFlags;
        Vector3D DecorationOffset = new Vector3D();
        Vector3D DecorationScale = new Vector3D( 1.0f, 1.0f, 1.0f );
        readonly List<BlockData> BlocksData = new List<BlockData>();

        public static byte[] DecryptChunkData( byte[] cryptedChunkData )
        {
            if ( cryptedChunkData == null )
            {
                return null;
            }

            for ( uint offset = 0; offset < cryptedChunkData.Length; offset++ )
            {
                uint data = cryptedChunkData[ offset ];
                uint hash = ( uint )( cryptedChunkData.Length * ( ( cryptedChunkData.Length * 2 ) - offset ) );

                hash ^= 0xead9c8b3;
                hash += offset * 3 % 0x7f;

                if ( offset % 5 < 2 )
                {
                    hash = ~hash;
                }

                cryptedChunkData[ offset ] = ( byte )~( data ^ hash );
            }

            return cryptedChunkData;
        }

        public override TrackVersion GetTrackVersion()
        {
            return TrackVersion.Unlimiter13;
        }

#pragma warning disable CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.
        public override void ArchiveOld( GameBoxReader reader )
        {
            this.ChallengeFlags = ( ChallengeFlags )reader.ReadUInt16();

            if ( this.ChallengeFlags.HasFlag( ChallengeFlags.ReservedBit ) )
            {
                return;
            }

            if ( !this.ChallengeFlags.HasFlag( ChallengeFlags.DecorationVisibility_Nothing ) )
            {
                if ( this.ChallengeFlags.HasFlag( ChallengeFlags.IsDecorationMoved ) )
                {
                    this.DecorationOffset = reader.ReadVec3D();
                }

                if ( this.ChallengeFlags.HasFlag( ChallengeFlags.IsDecorationScaled ) )
                {
                    this.DecorationScale = reader.ReadVec3D();
                }
            }

            uint blockCount = reader.ReadUInt32();

            while ( blockCount-- != 0 )
            {
                BlockData blockData = new BlockData( reader.ReadUInt32() );
                this.BlocksData.Add( blockData );

                BlockFlags blockFlags = ( BlockFlags )reader.ReadUInt16();

                // outside boundaries
                if ( blockFlags.HasFlag( BlockFlags.IsOutsideBoundaries ) )
                {
                    blockData.InternalOverOverSizeChunkX = reader.ReadByte();
                    blockData.InternalOverOverSizeChunkY = reader.ReadByte();
                    blockData.InternalOverOverSizeChunkZ = reader.ReadByte();
                }

                blockData.InternalIsInverted = blockFlags.HasFlag( BlockFlags.IsInverted );
                blockData.InternalIsDynamic = blockFlags.HasFlag( BlockFlags.IsDynamic );
                blockData.InternalIsInvisible = blockFlags.HasFlag( BlockFlags.IsInvisible );
                blockData.InternalIsCollisionDisabled = blockFlags.HasFlag( BlockFlags.IsCollisionDisabled );
                blockData.InternalIsSpawnPointFixEnabled = blockFlags.HasFlag( BlockFlags.IsSpawnPointFixEnabled );

                if ( blockFlags.HasFlag( BlockFlags.IsVanillaTerrain ) )
                {
                    continue;
                }

                blockData.InternalIsClassicTerrain = blockFlags.HasFlag( BlockFlags.IsClassicTerrain );

                if ( blockFlags.HasFlag( BlockFlags.IsMoved ) )
                {
                    blockData.InternalBlockOffset = reader.ReadVec3D();
                }

                if ( blockFlags.HasFlag( BlockFlags.IsRotated ) )
                {
                    blockData.InternalBlockRotation = reader.ReadVec3D();
                }

                if ( blockFlags.HasFlag( BlockFlags.IsScaled ) )
                {
                    blockData.InternalBlockScale = reader.ReadVec3D();
                }

                if ( blockFlags.HasFlag( BlockFlags.HasIdentifier ) )
                {
                    blockData.InternalBlockGroup = reader.ReadString();
                }
            }

            // Skip unused value
            reader.Skip( 4 );
        }

        public override void ArchiveNew( GameBoxReader reader, uint archiveVersion )
        {
            // No need to implement this chunk (at the moment)
        }
#pragma warning restore CA1062 // Validate arguments of public methods -- Reader is always guaranteed to be non-null.

        public override TMUnlimiter.BlockData[] GetBlocksData()
        {
            return this.BlocksData.ToArray();
        }

        public override bool IsTrackBaseEmpty()
        {
            return this.ChallengeFlags.HasFlag( ChallengeFlags.IsTrackBaseEmpty );
        }

        public override bool IsPylonsDisabled()
        {
            return this.ChallengeFlags.HasFlag( ChallengeFlags.IsPylonsDisabled );
        }

        public override bool IsDecorationOffsetApplied()
        {
            return this.DecorationOffset.X != 0.0f && this.DecorationOffset.Y != 0.0f && this.DecorationOffset.Z != 0.0f;
        }

        public override bool IsDecorationScaleApplied()
        {
            return this.DecorationScale.X != 1.0f && this.DecorationScale.Y != 1.0f && this.DecorationScale.Z != 1.0f;
        }

        public override Vector3D GetDecorationOffset()
        {
            return new Vector3D( this.DecorationOffset.X, this.DecorationOffset.Y, this.DecorationOffset.Z );
        }

        public override Vector3D GetDecorationScale()
        {
            return new Vector3D( this.DecorationScale.X, this.DecorationScale.Y, this.DecorationScale.Z );
        }

        public override DecorationVisibility GetDecorationVisibility()
        {
            if ( this.ChallengeFlags.HasFlag( ChallengeFlags.DecorationVisibility_SkyOnly ) )
            {
                return DecorationVisibility.SkyOnly;
            }
            else if ( this.ChallengeFlags.HasFlag( ChallengeFlags.DecorationVisibility_Warp ) )
            {
                return DecorationVisibility.WarpOnly;
            }
            else if ( this.ChallengeFlags.HasFlag ( ChallengeFlags.DecorationVisibility_Nothing ) )
            {
                return DecorationVisibility.Nothing;
            }
            else
            {
                return DecorationVisibility.Default;
            }
        }
    }
}