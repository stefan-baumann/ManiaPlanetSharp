﻿using System;
using System.Collections.Generic;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;

namespace ManiaPlanetSharp.GameBox.Parsing.ParserGeneration.AutoGenerated
{
	public static partial class AutoGeneratedParsers
	{
		public static Dictionary<Type, IParser<object>> StructParsers { get; } = new Dictionary<Type, IParser<object>> {
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint), new CheckpointParser() },
			{ typeof(ManiaPlanetSharp.GameBox.Parsing.Chunks.Block), new BlockParser() },
		};
	}



	public class CheckpointParser
		: PregeneratedCustomStructParser<ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint>
	{
        public override ManiaPlanetSharp.GameBox.Parsing.Chunks.Checkpoint Parse(GameBoxReader reader)
        {
			var result = new Checkpoint();
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
			var result = new Block();
			result.Name = reader.ReadLookbackString();
			result.Rotation = reader.ReadByte();
			result.X = reader.ReadByte();
			result.Y = reader.ReadByte();
			result.Z = reader.ReadByte();
			result.FlagsU = reader.ReadUInt32();
			if (result.Flags.HasFlag((Enum)((BlockFlags)BlockFlags.CustomBlock)))
			{
			    result.Author = reader.ReadString();
			}
			if (result.Flags.HasFlag((Enum)((BlockFlags)BlockFlags.CustomBlock)))
			{
			    result.Skin = reader.ReadNode();
			}
			if (result.Flags.HasFlag((Enum)((BlockFlags)BlockFlags.HasBlockParameters)))
			{
			    result.BlockParameters = reader.ReadNode();
			}
			return result;
        }
	}
}
