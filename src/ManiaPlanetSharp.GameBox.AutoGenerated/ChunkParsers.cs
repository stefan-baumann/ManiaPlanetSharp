﻿using System;
using System.Collections.Generic;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration.AutoGenerated
{
	public static partial class AutoGeneratedParsers
	{
		public static Dictionary<Type, IChunkParser<Chunk>> ChunkParsers { get; } = new Dictionary<Type, IChunkParser<Chunk>> {
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointsChunk), new CheckpointsChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.MapChunk), new MapChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectAnchorChunk), new ObjectAnchorChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectBannerProfileChunk), new ObjectBannerProfileChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectCameraIndexChunk), new ObjectCameraIndexChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectGroundPointChunk), new ObjectGroundPointChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectModelChunk), new ObjectModelChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectTypeChunk), new ObjectTypeChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectUsabilityChunk), new ObjectUsabilityChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockSkinChunk), new BlockSkinChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelChunk), new VisualModelChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel1Chunk), new VisualModel1ChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel2Chunk), new VisualModel2ChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel3Chunk), new VisualModel3ChunkParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.WaypointSpecialPropertyChunk), new WaypointSpecialPropertyChunkParser() },
		};
	}



	public class CheckpointsChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointsChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x03043017 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointsChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointsChunk() { Id = reader.ReadUInt32() };
			result.Checkpoints = new ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint[reader.ReadUInt32()];
			for (int i = 0; i < result.Checkpoints.Length; i++)
			{
			    result.Checkpoints[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint>().Parse(reader);
			}
			return result;
        }
	}

	public class MapChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.MapChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x0304301F };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.MapChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.MapChunk() { Id = reader.ReadUInt32() };
			result.Uid = reader.ReadLookbackString();
			result.Environment = reader.ReadLookbackString();
			result.Author = reader.ReadLookbackString();
			result.Name = reader.ReadString();
			result.TimeOfDay = reader.ReadLookbackString();
			result.DecorationEnvironment = reader.ReadLookbackString();
			result.DecorationEnvironmentAuthor = reader.ReadLookbackString();
			result.Size = reader.ReadSize3D();
			result.NeedsUnlock = reader.ReadBool();
			result.Version = reader.ReadUInt32();
			result.Blocks = result.ParseBlocks(reader);
			return result;
        }
	}

	public class ObjectAnchorChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectAnchorChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002017 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectAnchorChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectAnchorChunk() { Id = reader.ReadUInt32() };
			result.Version = reader.ReadInt32();
			result.IsFreelyAnchorable = reader.ReadBool();
			return result;
        }
	}

	public class ObjectBannerProfileChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectBannerProfileChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002010 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectBannerProfileChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectBannerProfileChunk() { Id = reader.ReadUInt32() };
			result.BannerProfile = reader.ReadFileReference();
			return result;
        }
	}

	public class ObjectCameraIndexChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectCameraIndexChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002006 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectCameraIndexChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectCameraIndexChunk() { Id = reader.ReadUInt32() };
			result.DefaultCameraIndex = reader.ReadUInt32();
			return result;
        }
	}

	public class ObjectGroundPointChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectGroundPointChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002012 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectGroundPointChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectGroundPointChunk() { Id = reader.ReadUInt32() };
			result.GroundPoint = reader.ReadVec3D();
			result.PainterGroundMargin = reader.ReadFloat();
			result.OrbitalCenterHeightFromGround = reader.ReadFloat();
			result.OrbitalRadiusBase = reader.ReadFloat();
			result.OrbitalPreviewAngle = reader.ReadFloat();
			return result;
        }
	}

	public class ObjectModelChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectModelChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002019 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectModelChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectModelChunk() { Id = reader.ReadUInt32() };
			result.Version = reader.ReadInt32();
			result.PhysicalModel = reader.ReadNodeReference();
			result.VisualModel = reader.ReadNodeReference();
			if (result.Version == 1)
			{
			    result.VisualModelStatic = reader.ReadNodeReference();
			}
			if (result.Version >= 3)
			{
			    result.Unknown1 = reader.ReadInt32();
			}
			if (result.Version >= 4)
			{
			    result.Unknown2 = reader.ReadNodeReference();
			}
			if (result.Version >= 5)
			{
			    result.Unknown3 = reader.ReadNode();
			}
			if (result.Version >= 6)
			{
			    result.UnknownCount = reader.ReadInt32();
			}
			if (result.Version >= 7)
			{
			    result.Unknown4 = reader.ReadInt32();
			}
			if (result.Version >= 8)
			{
			    result.Unknown5 = reader.ReadNodeReference();
			}
			if (result.Version >= 8 && result.Unknown5 == null)
			{
			    result.Unknown6 = reader.ReadNodeReference();
			}
			return result;
        }
	}

	public class ObjectTypeChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectTypeChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002015 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectTypeChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectTypeChunk() { Id = reader.ReadUInt32() };
			result.ObjectTypeU = reader.ReadUInt32();
			return result;
        }
	}

	public class ObjectUsabilityChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectUsabilityChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E002018 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectUsabilityChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ObjectUsabilityChunk() { Id = reader.ReadUInt32() };
			result.Version = reader.ReadInt32();
			result.IsUsable = reader.ReadBool();
			return result;
        }
	}

	public class BlockSkinChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockSkinChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x03059000, 0x03059001, 0x03059002 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockSkinChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockSkinChunk() { Id = reader.ReadUInt32() };
			if (result.ChunkId == 0)
			{
			    result.Ignored = reader.ReadString();
			}
			if (result.ChunkId >= 1)
			{
			    result.Pack = reader.ReadFileReference();
			}
			if (result.ChunkId >= 2)
			{
			    result.ParentPack = reader.ReadFileReference();
			}
			return result;
        }
	}

	public class VisualModelChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E007000 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelChunk() { Id = reader.ReadUInt32() };
			result.Part1 = reader.ReadNodeReference();
			result.Part2 = reader.ReadNodeReference();
			return result;
        }
	}

	public class VisualModel1ChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel1Chunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E007001 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel1Chunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel1Chunk() { Id = reader.ReadUInt32() };
			result.Version = reader.ReadInt32();
			if (result.Version == 1)
			{
			    result.Unknown1 = reader.ReadInt32();
			}
			if (result.Version == 1)
			{
			    result.Unknown2 = reader.ReadInt32();
			}
			if (result.Version == 1)
			{
			    result.Unknown3 = reader.ReadFloat();
			}
			if (result.Version >= 9)
			{
			    result.Path1 = reader.ReadString();
			}
			if (result.Version >= 9 && result.Path1 == null)
			{
			    result.Unknown4 = reader.ReadNodeReference();
			}
			if (result.Version <= 7)
			{
			    result.Unknown5 = reader.ReadNodeReference();
			}
			if (result.Version < 18)
			{
			    result.Unknown6 = reader.ReadNodeReference();
			}
			if (result.Version >= 2 && result.Version < 9)
			{
			    result.Path2 = reader.ReadString();
			}
			if (result.Version >= 2 && result.Version < 5)
			{
			    result.Unknown7Count = reader.ReadInt32();
			}
			if (result.Version >= 2 && result.Version < 5)
			{
			    result.Unknown7 = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1[(uint)result.Unknown7Count];
			    for (int i = 0; i < result.Unknown7.Length; i++)
			    {
			        result.Unknown7[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1>().Parse(reader);
			    }
			}
			if (result.Version == 5)
			{
			    result.Unknown8Count = reader.ReadInt32();
			}
			if (result.Version == 5)
			{
			    result.Unknown8 = new ManiaPlanetSharp.GameBox.Node[(uint)result.Unknown8Count];
			    for (int i = 0; i < result.Unknown8.Length; i++)
			    {
			        result.Unknown8[i] = reader.ReadNodeReference();
			    }
			}
			if (result.Version > 5)
			{
			    result.Unknown9 = reader.ReadInt32();
			}
			if (result.Version >= 2)
			{
			    result.Unknown10Count = reader.ReadInt32();
			}
			if (result.Version >= 2)
			{
			    result.Unknown10 = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1[(uint)result.Unknown10Count];
			    for (int i = 0; i < result.Unknown10.Length; i++)
			    {
			        result.Unknown10[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1>().Parse(reader);
			    }
			}
			if (result.Version >= 2 && result.Version <= 16)
			{
			    result.Unknown11 = reader.ReadString();
			}
			if (result.Version >= 2 && result.Version <= 16)
			{
			    result.Unknown12 = reader.ReadString();
			}
			if (result.Version >= 2 && result.Version <= 16)
			{
			    result.Unknown13 = reader.ReadString();
			}
			if (result.Version >= 10)
			{
			    result.Unknown14 = reader.ReadString();
			}
			if (result.Version >= 11 && result.Unknown14 != null)
			{
			    result.Unknown15 = new System.Single[3];
			    for (int i = 0; i < result.Unknown15.Length; i++)
			    {
			        result.Unknown15[i] = reader.ReadFloat();
			    }
			}
			if (result.Version >= 12)
			{
			    result.Unknown16 = reader.ReadString();
			}
			if (result.Version >= 12 && result.Unknown16 != null)
			{
			    result.Unknown17 = reader.ReadInt32();
			}
			if (result.Version == 8)
			{
			    result.Unknown18 = reader.ReadNodeReference();
			}
			if (result.Version >= 13)
			{
			    result.Unknown19Count = reader.ReadInt32();
			}
			if (result.Version >= 13)
			{
			    result.Unknown19 = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3[(uint)result.Unknown19Count];
			    for (int i = 0; i < result.Unknown19.Length; i++)
			    {
			        result.Unknown19[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3>().Parse(reader);
			    }
			}
			if (result.Version >= 13)
			{
			    result.Unknown20Count = reader.ReadInt32();
			}
			if (result.Version >= 13)
			{
			    result.Unknown20 = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct4[(uint)result.Unknown20Count];
			    for (int i = 0; i < result.Unknown20.Length; i++)
			    {
			        result.Unknown20[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct4>().Parse(reader);
			    }
			}
			if (result.Version >= 14)
			{
			    result.Unknown21 = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3>().Parse(reader);
			}
			if (result.Version == 15)
			{
			    result.Unknown22 = reader.ReadString();
			}
			if (result.Version >= 16)
			{
			    result.Unknown23 = reader.ReadNodeReference();
			}
			if (result.Version >= 19)
			{
			    result.Unknown24 = reader.ReadFloat();
			}
			if (result.Version >= 20)
			{
			    result.Unknown25 = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3>().Parse(reader);
			}
			if (result.Version >= 21)
			{
			    result.Unknown26 = reader.ReadFileReference();
			}
			return result;
        }
	}

	public class VisualModel2ChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel2Chunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E007002 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel2Chunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel2Chunk() { Id = reader.ReadUInt32() };
			result.Version = reader.ReadInt32();
			result.Unknown1 = reader.ReadString();
			result.Unknown2 = reader.ReadString();
			result.Unknown3 = reader.ReadString();
			result.Unknown4 = reader.ReadString();
			result.Unknown5 = reader.ReadString();
			result.Unknown6 = new System.Single[12];
			for (int i = 0; i < result.Unknown6.Length; i++)
			{
			    result.Unknown6[i] = reader.ReadFloat();
			}
			return result;
        }
	}

	public class VisualModel3ChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel3Chunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E007003 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel3Chunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModel3Chunk() { Id = reader.ReadUInt32() };
			result.Node = reader.ReadNodeReference();
			return result;
        }
	}

	public class WaypointSpecialPropertyChunkParser
		: PregeneratedChunkParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.WaypointSpecialPropertyChunk>
	{
		public override List<uint> ParseableIds => new List<uint>() { 0x2E009000 };

        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.WaypointSpecialPropertyChunk Parse(GameBoxReader reader, uint chunkId)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.WaypointSpecialPropertyChunk() { Id = reader.ReadUInt32() };
			result.Version = reader.ReadUInt32();
			if (result.Version == 1)
			{
			    result.Spawn = reader.ReadUInt32();
			}
			if (result.Version > 1)
			{
			    result.Tag = reader.ReadString();
			}
			result.Order = reader.ReadUInt32();
			return result;
        }
	}
}
