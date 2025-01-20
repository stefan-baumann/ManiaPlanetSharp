using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03092019)]
    public class GhostChunk
        : Chunk
    {
        [Property]
        public uint EventsDuration { get; set; }

        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint Ignored { get; set; }

        [Property(SpecialPropertyType.LookbackString), Array, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public string[] ControlNames { get; set; }

        [Property]
        [EditorBrowsable(EditorBrowsableState.Never), Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint ControlEntryCount { get; set; }

        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint Unknown { get; set; }

        [Property, Array(nameof(ControlEntryCount)), Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public GhostControlEntry[] ControlEntries { get; set; }

        [Property(SpecialPropertyType.LongString), Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public string GameVersion { get; set; }

        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint ExecutableChecksum { get; set; }

        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint OSKind { get; set; }

        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint CPUKind { get; set; }

        private string raceSettingsXmlString;
        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public string RaceSettingsXmlString
        {
            get
            {
                return this.raceSettingsXmlString;
            }
            set
            {
                this.raceSettingsXmlString = value;
                //XmlSerializer serializer = new XmlSerializer(typeof(GhostRaceSettingsRoot));
                //using (StringReader stringReader = new StringReader(this.raceSettingsXmlString.Replace("&", "&amp;")))
                //{
                //    this.Root = (GhostRaceSettingsRoot)serializer.Deserialize(stringReader);
                //}
            }
        }

        //public GhostRaceSettingsRoot Root { get; set; }

        [Property, Condition(nameof(EventsDuration), ConditionOperator.GreaterThan, 0)]
        public uint Unknown2 { get; set; }
    }

    [CustomStruct]
    public class GhostControlEntry
    {
        [Property]
        public uint TimeU { get; set; }

        public TimeSpan Time => TimeSpan.FromMilliseconds(this.TimeU - 100000);

        [Property]
        public byte ControlNameIndex { get; set; }

        [Property]
        public uint OnOffU { get; set; }

        public bool OnOff => this.OnOffU > 0;
    }
}
