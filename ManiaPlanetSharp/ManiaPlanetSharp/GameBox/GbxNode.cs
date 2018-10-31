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

        public virtual uint Class { get; set; }

        public virtual byte[] Data { get; set; }

        protected virtual List<GbxNode> Nodes { get; private set; } = new List<GbxNode>();

        public virtual GbxNode this[uint id]
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

        public virtual int Count { get => this.Nodes.Count; }

        public virtual void Add(GbxNode chunk)
        {
            this.Nodes.Add(chunk);
        }

        public MemoryStream GetDataStream()
        {
            if (this.Data == null)
            {
                throw new ArgumentNullException();
            }
            return new MemoryStream(this.Data);
        }

        public virtual void JoinWith(GbxNode other)
        {
            this.Nodes.AddRange(other);
        }

        public virtual IEnumerator<GbxNode> GetEnumerator()
        {
            return ((IEnumerable<GbxNode>)Nodes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<GbxNode>)this).GetEnumerator();
        }
    }
}
