using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxFile
        : GbxNode
    {
        public GbxHeader Header { get; set; }
        public GbxReferenceTable ReferenceTable { get; set; }
        public GbxNode Body { get; set; }

        public override void Add(GbxNode chunk)
        {
            throw new InvalidOperationException();
        }

        public override int Count => 3;

        public override IEnumerator<GbxNode> GetEnumerator()
        {
            return ((IEnumerable<GbxNode>)new GbxNode[] { this.Header, this.ReferenceTable, this.Body }).GetEnumerator();
        }
    }
}
