using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ManiaPlanetSharp.GameBoxView
{
    public class MacroblockMetadataTreeNode
        : MetadataTreeNode
    {
        private const string NotAvailable = "not available";

        public MacroblockMetadataTreeNode(MacroblockMetadataProvider macroblock)
            : base("Macroblock Metadata")
        {
            this.Macroblock = macroblock;
            this.InitializeNodes();
        }

        public MacroblockMetadataProvider Macroblock { get; private set; }

        protected override IEnumerable<TextTreeNode> GetNodes()
        {
            yield return new FormattedTextTreeNode("Name", this.Macroblock.Name ?? NotAvailable);
            yield return new FormattedTextTreeNode("Description", string.IsNullOrWhiteSpace(this.Macroblock.Description) ? "no description" : this.Macroblock.Description);
            yield return new FormattedTextTreeNode("Author", string.IsNullOrWhiteSpace(this.Macroblock.Author) ? NotAvailable : this.Macroblock.Author);

            yield return new TextTreeNode("Collection", this.Macroblock.Collection ?? NotAvailable);
            yield return new TextTreeNode("Path", this.Macroblock.Path ?? NotAvailable);

            var icon = this.Macroblock.GenerateIconBitmap();
            if (icon != null)
            {
                var image = this.ImageSourceFromImage(this.Macroblock.GenerateIconBitmap());
                yield return new TextTreeNode("Icon", $"{image.Width:#0}x{image.Height:#0}")
                {
                    HideValueWhenExpanded = true,
                    Nodes = new ObservableCollection<TextTreeNode>()
                    {
                        new ImageTreeNode(image, new System.Windows.Size(image.Width, image.Height)),
                        new TextTreeNode("Size", $"{image.Width:#0}x{image.Height:#0}"),
                        new TextTreeNode("Auto-Generated", this.Macroblock.UseAutoRenderedIcon != null ? this.Macroblock.UseAutoRenderedIcon.Value ? "True" : "False" : NotAvailable),
                        new TextTreeNode("Icon Quarter-Rotations", this.Macroblock.IconQuarterRotations?.ToString() ?? NotAvailable)
                    }
                };
            }
            else
            {
                yield return new TextTreeNode("Icon", "no icon");
            }

            yield return new TextTreeNode("Product State", this.Macroblock.ProductState?.ToString() ?? NotAvailable);

            yield break;
        }

        protected ImageSource ImageSourceFromImage(System.Drawing.Image source)
        {
            using var ms = new MemoryStream();
            source.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }
    }
}
