namespace ManiaPlanetSharp.GameBoxView
{
    public class HyperlinkTreeNode
        : TextTreeNode
    {
        public HyperlinkTreeNode(string name, string url)
            : this(name, url, url)
        { }

        public HyperlinkTreeNode(string name, string text, string url)
            : base(name, text)
        {
            this.Url = url;
        }

        public virtual string Url { get; private set; }
    }
}
