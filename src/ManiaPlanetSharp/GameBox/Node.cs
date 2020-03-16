using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class Node
        //: IEnumerable<Node>, IList<Node>
    {
        public Node()
        { }

        public Node(uint id)
            : this()
        {
            this.Id = id;
        }

        public Node(uint id, byte[] data)
            : this(id)
        {
            this.Data = data;
        }

        //public Node(uint id, IEnumerable<Node> nodes)
        //    : this(id)
        //{
        //    this.Nodes.AddRange(nodes);
        //}

        //public Node(uint id, byte[] data, IEnumerable<Node> nodes)
        //    : this(id, data)
        //{
        //    this.Nodes.AddRange(nodes);
        //}



        public virtual uint Id { get; set; }
        public virtual uint ClassId => this.Id & 0xFFFFF000;
        public virtual uint ChunkId => this.Id & 0xFFF;

        public byte[] Data { get; set; }

        //protected virtual List<Node> Nodes { get; private set; } = new List<Node>();

        //public int Count => this.Nodes.Count;

        //public bool IsReadOnly => false;

        //public Node this[int index]
        //{
        //    get => this.Nodes[index];
        //    set => this.Nodes[index] = value;
        //}



        public virtual string GetClassName()
        {
            return KnownClassIds.GetClassName(this.ClassId);
        }

        public virtual Stream GetStream()
        {
            if (this.Data == null) throw new ArgumentNullException();
            return new MemoryStream(this.Data);
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} (0x{this.Id:X8}/{this.GetClassName()})";
        }

        //public IEnumerator<Node> GetEnumerator() => this.Nodes.GetEnumerator();
        //IEnumerator IEnumerable.GetEnumerator() => this.Nodes.GetEnumerator();

        //public int IndexOf(Node item) => this.Nodes.IndexOf(item);
        //public void Insert(int index, Node item) => this.Nodes.Insert(index, item);
        //public void RemoveAt(int index) => this.Nodes.RemoveAt(index);
        //public void Add(Node item) => this.Nodes.Add(item);
        //public void AddRange(IEnumerable<Node> collection) => this.Nodes.AddRange(collection);
        //public void Clear() => this.Nodes.Clear();
        //public bool Contains(Node item) => this.Nodes.Contains(item);
        //public void CopyTo(Node[] array, int arrayIndex) => this.Nodes.CopyTo(array, arrayIndex);
        //public bool Remove(Node item) => this.Nodes.Remove(item);
    }
}
