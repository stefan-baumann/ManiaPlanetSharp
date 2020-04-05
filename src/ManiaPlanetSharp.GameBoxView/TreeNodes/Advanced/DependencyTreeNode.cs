using ManiaPlanetSharp.GameBox.Parsing.Chunks;

namespace ManiaPlanetSharp.GameBoxView
{
    public class DependencyTreeNode
        : TextTreeNode
    {
        public DependencyTreeNode(Dependency dependency)
            : base($"{System.IO.Path.GetFileName(dependency?.File)} ({(string.IsNullOrWhiteSpace(dependency.Url) ? "local" : "remote")})")
        {
            this.Dependency = dependency;
            if (this.Dependency != null)
            {
                this.Nodes.Add(new TextTreeNode("Path", dependency.File));
                if (!string.IsNullOrWhiteSpace(dependency.Url))
                    this.Nodes.Add(new HyperlinkTreeNode("Url", dependency.Url));
            }
        }

        public virtual Dependency Dependency { get; private set; }
    }
}
