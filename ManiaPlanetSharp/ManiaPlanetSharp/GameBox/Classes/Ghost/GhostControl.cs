using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Ghost
{
    public struct ControlEntry
    {
        public uint Time { get; set; }
        public byte ControlNameIndex { get; set; }
        public uint OnOff { get; set; }
    }

    public class GhostControl
        : Node
    {
        public uint EventDuration { get; set; }
        public uint Ignored1 { get; set; }
        public uint ControlNameCount { get; set; }
        public string[] ControlNames { get; set; }
        public uint Ignored2 { get; set; }
        public uint ControlEntryCount { get; set; }
        public ControlEntry[] ControlEntries { get; set; }
        public string GameVersion { get; set; }
        public uint ExecutableChecksum { get; set; }
        public uint OperatingSystemKind { get; set; }
        public uint CpuKind { get; set; }
        public string RaceSettings { get; set; }
        public uint Ignored3 { get; set; }
    }

    public class GhostControlParser
        : ClassParser<GhostControl>
    {
        protected override int ChunkId => 0x03092019;

        protected override GhostControl ParseChunkInternal(GameBoxReader reader)
        {
            var result = new GhostControl();
            result.EventDuration = reader.ReadUInt32();
            result.Ignored1 = reader.ReadUInt32();

            result.ControlNameCount = reader.ReadUInt32();
            result.ControlNames = new string[result.ControlNameCount];
            for (int i = 0; i < result.ControlNameCount; i++)
            {
                result.ControlNames[i] = reader.ReadLookbackString();
            }

            result.ControlEntryCount = reader.ReadUInt32();
            result.Ignored2 = reader.ReadUInt32();
            result.ControlEntries = new ControlEntry[result.ControlEntryCount];
            for (int i = 0; i < result.ControlEntryCount; i++)
            {
                result.ControlEntries[i] = new ControlEntry()
                {
                    Time = reader.ReadUInt32(),
                    ControlNameIndex = reader.ReadByte(),
                    OnOff = reader.ReadUInt32()
                };
            }

            result.GameVersion = reader.ReadString();
            result.ExecutableChecksum = reader.ReadUInt32();
            result.OperatingSystemKind = reader.ReadUInt32();
            result.CpuKind = reader.ReadUInt32();
            result.RaceSettings = reader.ReadString();
            result.Ignored3 = reader.ReadUInt32();

            return result;
        }
    }
}
