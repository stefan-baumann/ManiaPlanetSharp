using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GameBoxFile
        : Node
    {
        public FileHeader Header { get; set; }
        public ReferenceTable ReferenceTable { get; set; }
        public Node Body { get; set; }

        public override void Add(Node chunk)
        {
            throw new InvalidOperationException();
        }

        public override int Count => 3;

        public override IEnumerator<Node> GetEnumerator()
        {
            return ((IEnumerable<Node>)new Node[] { this.Header, this.ReferenceTable, this.Body }).GetEnumerator();
        }
    }
}
