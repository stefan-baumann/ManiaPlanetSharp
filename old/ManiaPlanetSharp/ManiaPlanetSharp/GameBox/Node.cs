using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ManiaPlanetSharp.GameBox
{
    public class Node
        : IEnumerable<Node>, IEnumerable
    {
        public Node()
        { }

        public Node(uint @class)
            : this()
        {
            this.Class = @class;
        }

        public virtual uint Class { get; set; }

        public virtual byte[] Data { get; set; }

        protected virtual List<Node> Nodes { get; private set; } = new List<Node>();

        public virtual Node this[uint id]
        {
            get
            {
                Node result = this.Nodes.FirstOrDefault(node => node.Class == id || (node.Class & 0xfff) == id);
                if (result == null)
                {
                    throw new KeyNotFoundException("No node matching the given id was found");
                }
                return result;
            }
        }

        public virtual int Count { get => this.Nodes.Count; }

        public virtual void Add(Node chunk)
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

        public virtual void JoinWith(Node other)
        {
            this.Nodes.AddRange(other);
        }

        public virtual IEnumerator<Node> GetEnumerator()
        {
            return ((IEnumerable<Node>)Nodes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Node>)this).GetEnumerator();
        }
    }
}
