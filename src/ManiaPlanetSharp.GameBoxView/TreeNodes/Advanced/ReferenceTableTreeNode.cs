using ManiaPlanetSharp.GameBox;

namespace ManiaPlanetSharp.GameBoxView
{
    public class ReferenceTableTreeNode
        : TextTreeNode
    {
        public ReferenceTableTreeNode(ReferenceTableExternalNode node)
            : base(node.FileName)
        {
            this.Node = node;

            //this.HideValueWhenExpanded = true;

            if (!string.IsNullOrWhiteSpace(this.Node?.FileName))
                new TextTreeNode("File Name", this.Node.FileName);
            new TextTreeNode("Resource Index", this.Node.ResourceIndex.ToString());
            new TextTreeNode("Node Index", this.Node.NodeIndex.ToString());
            new TextTreeNode("Folder Index", this.Node.FolderIndex.ToString());
            new TextTreeNode("Use File", this.Node.UseFile ? "True" : "False");
        }

        public ReferenceTableExternalNode Node { get; set; }
    }
}
