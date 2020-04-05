using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ManiaPlanetSharp.GameBoxView
{
    public abstract class MetadataTreeNode
        : TextTreeNode
    {
        protected MetadataTreeNode(string name)
            : base(name)
        { }

        protected void InitializeNodes()
        {
            this.Nodes = new ObservableCollection<TextTreeNode>(this.GetNodes().CatchExceptions());
        }

        protected abstract IEnumerable<TextTreeNode> GetNodes();
    }
}
