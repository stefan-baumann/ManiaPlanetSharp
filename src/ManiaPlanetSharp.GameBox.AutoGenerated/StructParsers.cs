﻿using System;
using System.Collections.Generic;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration.AutoGenerated
{
	public static partial class AutoGeneratedParsers
	{
		public static Dictionary<Type, IParser<object>> StructParsers { get; } = new Dictionary<Type, IParser<object>> {
			{ typeof(ManiaPlanetSharp.GameBox.GameBoxFile), new GameBoxFileParser() },
			{ typeof(ManiaPlanetSharp.GameBox.HeaderEntry), new HeaderEntryParser() },
			{ typeof(ManiaPlanetSharp.GameBox.ReferenceTableFolder), new ReferenceTableFolderParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.CollectorStock), new CollectorStockParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointEntry), new CheckpointEntryParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostControlEntry), new GhostControlEntryParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostData), new GhostDataParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostSampleRecord), new GhostSampleRecordParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint), new CheckpointParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.Block), new BlockParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.EmbeddedItem), new EmbeddedItemParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.SkinFile), new SkinFileParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1), new VisualModelStruct1Parser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct2), new VisualModelStruct2Parser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3), new VisualModelStruct3Parser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct4), new VisualModelStruct4Parser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkHeaderContent), new ReplayMainChunkHeaderContentParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkBodyContent), new ReplayMainChunkBodyContentParser() },
		};
	}



	public class GameBoxFileParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.GameBoxFile>
	{
        public override ManiaPlanetSharp.GameBox.GameBoxFile Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.GameBoxFile();
			result.MagicString = result.ReadMagicString(reader);
			result.Version = reader.ReadUInt16();
			if (result.Version >= 3)
			{
			    result.FileFormatC = reader.ReadChar();
			}
			if (result.Version >= 3)
			{
			    result.ReferenceTableCompressedC = reader.ReadChar();
			}
			if (result.Version >= 3)
			{
			    result.BodyCompressedC = reader.ReadChar();
			}
			if (result.Version >= 4)
			{
			    result.Unused = reader.ReadChar();
			}
			if (result.Version >= 3)
			{
			    result.MainClassId = reader.ReadUInt32();
			}
			if (result.Version >= 6)
			{
			    result.UserDataSize = reader.ReadUInt32();
			}
			if (result.Version >= 6 && result.UserDataSize > 0)
			{
			    result.HeaderChunkCount = reader.ReadUInt32();
			}
			if (result.HeaderChunkCount > 0)
			{
			    result.HeaderChunkEntries = result.ParseHeaderChunkEntries(reader);
			}
			if (result.HeaderChunkCount > 0)
			{
			    result.HeaderChunks = result.ParseHeaderChunks(reader);
			}
			if (result.Version >= 6)
			{
			    result.NodeCount = reader.ReadUInt32();
			}
			result.ReferenceTableExternalNodeCount = reader.ReadUInt32();
			if (result.ReferenceTableExternalNodeCount > 0)
			{
			    result.ReferenceTableAncestorLevel = reader.ReadUInt32();
			}
			if (result.ReferenceTableExternalNodeCount > 0)
			{
			    result.ReferenceTableFolders = new ManiaPlanetSharp.GameBox.ReferenceTableFolder[reader.ReadUInt32()];
			    for (int i = 0; i < result.ReferenceTableFolders.Length; i++)
			    {
			        result.ReferenceTableFolders[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.ReferenceTableFolder>().Parse(reader);
			    }
			}
			if (result.ReferenceTableExternalNodeCount > 0)
			{
			    result.ReferenceTableExternalNodes = new ManiaPlanetSharp.GameBox.ReferenceTableExternalNode[(uint)result.ReferenceTableExternalNodeCount];
			    for (int i = 0; i < result.ReferenceTableExternalNodes.Length; i++)
			    {
			        result.ReferenceTableExternalNodes[i] = result.ParseReferenceTableExternalNode(reader);
			    }
			}
			if (result.BodyCompressed)
			{
			    result.UncompressedBodySize = reader.ReadUInt32();
			}
			if (result.BodyCompressed)
			{
			    result.CompressedBodySize = reader.ReadUInt32();
			}
			result.RawBodyData = result.ReadBodyData(reader);
			return result;
        }
	}

	public class HeaderEntryParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.HeaderEntry>
	{
        public override ManiaPlanetSharp.GameBox.HeaderEntry Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.HeaderEntry();
			result.ChunkID = reader.ReadUInt32();
			result.ChunkSizeU = reader.ReadUInt32();
			return result;
        }
	}

	public class ReferenceTableFolderParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.ReferenceTableFolder>
	{
        public override ManiaPlanetSharp.GameBox.ReferenceTableFolder Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.ReferenceTableFolder();
			result.Name = reader.ReadString();
			result.SubFolders = new ManiaPlanetSharp.GameBox.ReferenceTableFolder[reader.ReadUInt32()];
			for (int i = 0; i < result.SubFolders.Length; i++)
			{
			    result.SubFolders[i] = ParserFactory.GetCustomStructParser<ManiaPlanetSharp.GameBox.ReferenceTableFolder>().Parse(reader);
			}
			return result;
        }
	}

	public class CollectorStockParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.CollectorStock>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.CollectorStock Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.CollectorStock();
			result.BlockName = reader.ReadLookbackString();
			result.Collection = reader.ReadLookbackString();
			result.Author = reader.ReadLookbackString();
			result.Data = reader.ReadUInt32();
			return result;
        }
	}

	public class CheckpointEntryParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointEntry>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointEntry Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.CheckpointEntry();
			result.TimeU = reader.ReadUInt32();
			result.Flags = reader.ReadUInt32();
			return result;
        }
	}

	public class GhostControlEntryParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostControlEntry>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostControlEntry Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostControlEntry();
			result.TimeU = reader.ReadUInt32();
			result.ControlNameIndex = reader.ReadByte();
			result.OnOffU = reader.ReadUInt32();
			return result;
        }
	}

	public class GhostDataParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostData>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostData Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostData();
			result.ClassId = reader.ReadUInt32();
			result.SkipList2 = reader.ReadBool();
			result.Unknown1 = reader.ReadUInt32();
			result.SamplePeriod = reader.ReadUInt32();
			result.Unknown2 = reader.ReadUInt32();
			result.SampleData = reader.ReadRaw((int)reader.ReadUInt32());
			result.SampleCount = reader.ReadUInt32();
			if (result.SampleCount > 0)
			{
			    result.FirstSampleOffset = reader.ReadUInt32();
			}
			if (result.SampleCount > 1)
			{
			    result.SizePerSample = reader.ReadUInt32();
			}
			if (result.SizePerSample == 4294967295)
			{
			    result.SampleSizes = new System.UInt32[(uint)result.SampleSizeCount];
			    for (int i = 0; i < result.SampleSizes.Length; i++)
			    {
			        result.SampleSizes[i] = reader.ReadUInt32();
			    }
			}
			if (result.SkipList2 == false)
			{
			    result.SampleTimes = new System.Int32[reader.ReadUInt32()];
			    for (int i = 0; i < result.SampleTimes.Length; i++)
			    {
			        result.SampleTimes[i] = reader.ReadInt32();
			    }
			}
			return result;
        }
	}

	public class GhostSampleRecordParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostSampleRecord>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostSampleRecord Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.GhostSampleRecord();
			result.Position = reader.ReadVec3D();
			result.AngleU = reader.ReadUInt16();
			result.AxisHeadingS = reader.ReadInt16();
			result.AxisPitchS = reader.ReadInt16();
			result.SpeedS = reader.ReadInt16();
			result.VelocityHeadingS = reader.ReadSByte();
			result.VelocityPitchS = reader.ReadSByte();
			return result;
        }
	}

	public class CheckpointParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint();
			result.Value1 = reader.ReadUInt32();
			result.Value2 = reader.ReadUInt32();
			result.Value3 = reader.ReadUInt32();
			return result;
        }
	}

	public class BlockParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.Block>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.Block Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.Block();
			result.Name = reader.ReadLookbackString();
			result.Rotation = reader.ReadByte();
			result.X = reader.ReadByte();
			result.Y = reader.ReadByte();
			result.Z = reader.ReadByte();
			result.FlagsU = reader.ReadUInt32();
			if (!result.Flags.HasFlag(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockFlags.Null) && result.Flags.HasFlag(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockFlags.CustomBlock))
			{
			    result.Author = reader.ReadString();
			}
			if (!result.Flags.HasFlag(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockFlags.Null) && result.Flags.HasFlag(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockFlags.CustomBlock))
			{
			    result.Skin = reader.ReadNodeReference();
			}
			if (!result.Flags.HasFlag(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockFlags.Null) && result.Flags.HasFlag(ManiaPlanetSharp.GameBox.Parsing.Chunks.BlockFlags.HasBlockParameters))
			{
			    result.BlockParameters = reader.ReadNodeReference();
			}
			return result;
        }
	}

	public class EmbeddedItemParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.EmbeddedItem>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.EmbeddedItem Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.EmbeddedItem();
			result.Path = reader.ReadLookbackString();
			result.Collection = reader.ReadLookbackString();
			result.Author = reader.ReadLookbackString();
			return result;
        }
	}

	public class SkinFileParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.SkinFile>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.SkinFile Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.SkinFile();
			return result;
        }
	}

	public class VisualModelStruct1Parser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1 Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct1();
			result.Version = reader.ReadInt32();
			result.Unknown1 = reader.ReadInt32();
			result.Unknown2 = reader.ReadInt32();
			result.Unknown3 = reader.ReadFloat();
			if (result.Version >= 1)
			{
			    result.Unknown4 = reader.ReadInt32();
			}
			if (result.Version >= 2)
			{
			    result.Unknown5 = reader.ReadInt32();
			}
			if (result.Version >= 2)
			{
			    result.Unknown6 = reader.ReadInt32();
			}
			return result;
        }
	}

	public class VisualModelStruct2Parser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct2>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct2 Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct2();
			result.Unknown1 = reader.ReadInt32();
			result.Unknown2 = new System.Single[7];
			for (int i = 0; i < result.Unknown2.Length; i++)
			{
			    result.Unknown2[i] = reader.ReadFloat();
			}
			return result;
        }
	}

	public class VisualModelStruct3Parser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3 Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct3();
			result.Unknown1 = reader.ReadString();
			if (result.Unknown1 == null)
			{
			    result.Unknown2 = reader.ReadNodeReference();
			}
			return result;
        }
	}

	public class VisualModelStruct4Parser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct4>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct4 Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.VisualModelStruct4();
			result.Unknown1 = reader.ReadFloat();
			result.Unknown2 = reader.ReadFloat();
			result.Unknown3 = reader.ReadFloat();
			result.Unknown4 = reader.ReadBool();
			result.Unknown5 = reader.ReadFloat();
			return result;
        }
	}

	public class ReplayMainChunkHeaderContentParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkHeaderContent>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkHeaderContent Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkHeaderContent();
			result.Version = reader.ReadUInt32();
			result.AuthorVersion = reader.ReadUInt32();
			result.Login = reader.ReadString();
			result.Nickname = reader.ReadString();
			result.Zone = reader.ReadString();
			result.ExtraInfo = reader.ReadString();
			return result;
        }
	}

	public class ReplayMainChunkBodyContentParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkBodyContent>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkBodyContent Parse(GameBoxReader reader)
        {
			var result = new ManiaPlanetSharp.GameBox.Parsing.Chunks.ReplayMainChunkBodyContent();
			result.MapData = reader.ReadRaw((int)reader.ReadUInt32());
			result.MapFile = result.ParseMap(reader);
			return result;
        }
	}
}
