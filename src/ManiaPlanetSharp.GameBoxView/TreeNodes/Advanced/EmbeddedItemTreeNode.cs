using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System.Collections.ObjectModel;
using System.Linq;

namespace ManiaPlanetSharp.GameBoxView
{
    public class EmbeddedItemTreeNode
        : TextTreeNode
    {
        public EmbeddedItemTreeNode(EmbeddedItem item, EmbeddedItemFile file)
            : base(System.IO.Path.GetFileName(item.Path))
        {
            this.Item = item;
            this.File = file;
            this.Nodes.Add(new TextTreeNode("Path", this.Item.Path));
            this.Nodes.Add(new TextTreeNode("Author", this.Item.Author));
            this.Nodes.Add(new TextTreeNode("Collection", this.Item.Collection));
            var analyzerNode = new ButtonTreeNode("Item File", $"Analyze ({file?.Data?.Length ?? 0} bytes)");
            analyzerNode.ButtonClicked += (s, e) =>
            {
                this.Nodes.Remove(analyzerNode);
                var file = this.File.Parse();
                var item = new ItemMetadataProvider(file);
                var fileNode = new TextTreeNode("Item File")
                {
                    IsExpanded = true,
                    Nodes = new ObservableCollection<TextTreeNode>()
                    {
                        new FileMetadataTreeNode(file),
                        new ItemMetadataTreeNode(item)
                    }
                };

                this.Nodes.Add(fileNode);
            };
            this.Nodes.Add(analyzerNode);
        }

        public EmbeddedItem Item { get; set; }
        public EmbeddedItemFile File { get; set; }
    }
}
