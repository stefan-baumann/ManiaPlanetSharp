using System.Windows;
using System.Windows.Media;

namespace ManiaPlanetSharp.GameBoxView
{
    public class ImageTreeNode
        : TextTreeNode
    {
        public ImageTreeNode(ImageSource imageSource, Size size)
            : base(null)
        {
            this.ImageSource = imageSource;
            this.Size = size;
        }

        public virtual ImageSource ImageSource { get; private set; }

        public Size Size { get; private set; }
    }
}
