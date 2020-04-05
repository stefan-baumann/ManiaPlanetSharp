using ManiaPlanetSharp.GameBox;
using System.Linq;

namespace ManiaPlanetSharp.GameBoxView
{
    public class FileReferenceTreeNode
        : TextTreeNode
    {
        public FileReferenceTreeNode(string name, FileReference value)
            : base(name)
        {
            this.Reference = value;

            if (this.Reference != null && !string.IsNullOrWhiteSpace(this.Reference.FilePath))
            {
                this.Nodes.Add(new TextTreeNode("Path", $"{this.Reference.FilePath}{(this.Reference.IsRelativePath ? " (relative)" : "")}"));
                this.Nodes.Add(new HyperlinkTreeNode("Url", this.Reference.LocatorUrl));
                this.Nodes.Add(new TextTreeNode("Checksum", string.Join("", this.Reference.Checksum.Select(b => b.ToString("X2")))));
            }
        }

        public FileReference Reference { get; private set; }

        public override string Value => System.IO.Path.GetFileName(this.Reference?.FilePath) ?? "None";
    }
}
