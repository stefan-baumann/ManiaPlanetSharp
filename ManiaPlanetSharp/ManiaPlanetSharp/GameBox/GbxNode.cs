using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxNode
        : IEnumerable<GbxNode>, IEnumerable
    {
        public GbxNode()
        { }

        public GbxNode(uint @class)
            : this()
        {
            this.Class = @class;
        }

        public uint Class { get; set; }

        public byte[] Data { get; set; }

        protected List<GbxNode> Nodes { get; private set; } = new List<GbxNode>();

        public GbxNode this[uint id]
        {
            get
            {
                GbxNode result = this.Nodes.FirstOrDefault(node => node.Class == id || (node.Class & 0xfff) == id);
                if (result == null)
                {
                    throw new KeyNotFoundException("No node matching the given id was found");
                }
                return result;
            }
        }

        public int Count { get => this.Nodes.Count; }

        public void Add(GbxNode chunk)
        {
            this.Nodes.Add(chunk);
        }

        IEnumerator<GbxNode> IEnumerable<GbxNode>.GetEnumerator()
        {
            return this.Nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Nodes.GetEnumerator();
        }

        public MemoryStream GetDataStream()
        {
            if (this.Data == null)
            {
                throw new ArgumentNullException();
            }
            return new MemoryStream(this.Data);
        }
    }
}
