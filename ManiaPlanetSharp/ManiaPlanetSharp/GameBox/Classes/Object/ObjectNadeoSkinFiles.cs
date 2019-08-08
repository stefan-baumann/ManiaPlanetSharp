using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectNadeoSkinFiles
        : Node
    {
        public uint NadeoSkinFidCount { get; set; }
        private Node[] fids;
        public Node[] Fids
        {
            get
            {
                return this.fids;
            }
            set
            {
                this.fids = value;
                if (this.fids != null)
                {
                    this.Nodes.Clear();
                    this.Nodes.AddRange(this.fids);
                }
            }
        }
    }

    public class ObjectNadeoSkinFilesParser
        : ClassParser<ObjectNadeoSkinFiles>
    {
        protected override int ChunkId => 0x2E002008;

        protected override ObjectNadeoSkinFiles ParseChunkInternal(GameBoxReader reader)
        {
            var result = new ObjectNadeoSkinFiles();
            result.NadeoSkinFidCount = reader.ReadUInt32();
            result.Fids = new Node[result.NadeoSkinFidCount];
            for (int i = 0; i < result.NadeoSkinFidCount; i++)
            {
                result.Fids[i] = reader.ReadNodeReference();
            }
            return result;
        }
    }
}
