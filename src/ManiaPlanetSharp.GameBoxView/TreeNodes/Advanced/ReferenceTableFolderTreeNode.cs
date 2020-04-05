using ManiaPlanetSharp.GameBox;
using System.Collections.ObjectModel;
using System.Linq;

namespace ManiaPlanetSharp.GameBoxView
{
    public class ReferenceTableFolderTreeNode
        : TextTreeNode
    {
        public ReferenceTableFolderTreeNode(ReferenceTableFolder rtf)
            : base(rtf.Name, $"{rtf.SubFolders.Length} subfolders")
        {
            this.ReferenceTableFolder = rtf;
            if (this.ReferenceTableFolder.SubFolders.Length > 0)
            {
                this.Nodes = new ObservableCollection<TextTreeNode>(this.ReferenceTableFolder.SubFolders.Select(rtf => new ReferenceTableFolderTreeNode(rtf)));
            }
        }
        public virtual ReferenceTableFolder ReferenceTableFolder { get; private set; }
    }
}
