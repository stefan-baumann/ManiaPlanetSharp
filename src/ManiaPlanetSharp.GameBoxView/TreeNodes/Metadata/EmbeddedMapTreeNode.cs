using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using System.Collections.Generic;

namespace ManiaPlanetSharp.GameBoxView.TreeNodes.Advanced
{
    public class EmbeddedMapTreeNode :
        MetadataTreeNode
    {
        public EmbeddedMapTreeNode(string name, GameBoxFile file):
            base(name)
        {
            this.File = file;
            this.InitializeNodes();
        }

        public GameBoxFile File { get; private set; }

        protected override IEnumerable<TextTreeNode> GetNodes()
        {
            var provider = new MapMetadataProvider(File);
            yield return new MapMetadataTreeNode(provider);
            yield return new FileMetadataTreeNode(File);
        }
    }
}
