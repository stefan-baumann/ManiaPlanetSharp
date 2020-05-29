using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ManiaPlanetSharp.GameBoxView
{
    public class ItemMetadataTreeNode
        : MetadataTreeNode
    {
        private const string NotAvailable = "not available";

        public ItemMetadataTreeNode(ItemMetadataProvider item)
            : base("Item Metadata")
        {
            this.Item = item;
            this.InitializeNodes();
        }

        public ItemMetadataProvider Item { get; private set; }

        protected override IEnumerable<TextTreeNode> GetNodes()
        {
            yield return new FormattedTextTreeNode("Name", this.Item.Name ?? NotAvailable);
            yield return new FormattedTextTreeNode("Description", string.IsNullOrWhiteSpace(this.Item.Description) ? "no description" : this.Item.Description);
            yield return new FormattedTextTreeNode("Author", string.IsNullOrWhiteSpace(this.Item.Author) ? NotAvailable : this.Item.Author);

            yield return new TextTreeNode("Collection", this.Item.Collection ?? NotAvailable);
            yield return new TextTreeNode("Type", this.Item.Type?.ToString() ?? NotAvailable);
            yield return new TextTreeNode("Path", this.Item.Path ?? NotAvailable);

            var icon = this.Item.GenerateIconBitmap();
            if (icon != null)
            {
                var image = this.ImageSourceFromImage(this.Item.GenerateIconBitmap());
                yield return new TextTreeNode("Icon", $"{image.Width:#0}x{image.Height:#0}")
                {
                    HideValueWhenExpanded = true,
                    Nodes = new ObservableCollection<TextTreeNode>()
                    {
                        new ImageTreeNode(image, new Size(image.Width, image.Height)),
                        new TextTreeNode("Size", $"{image.Width:#0}x{image.Height:#0}"),
                        new TextTreeNode("Auto-Generated", this.Item.UseAutoRenderedIcon != null ? this.Item.UseAutoRenderedIcon.Value ? "True" : "False" : NotAvailable),
                        new TextTreeNode("Icon Quarter-Rotations", this.Item.IconQuarterRotations?.ToString() ?? NotAvailable)
                    }
                };
            }
            else
            {
                yield return new TextTreeNode("Icon", "no icon");
            }

            var dependentFiles = new[] { ("Mesh", this.Item.MeshName), ("Shape", this.Item.ShapeName), ("TriggerShape", this.Item.TriggerShapeName) }.Where(t => t.Item2 != null).ToList();
            yield return new TextTreeNode("Dependent Files", $"{dependentFiles.Count} dependent files")
            {
                Nodes = new ObservableCollection<TextTreeNode>(dependentFiles.Select(t => new TextTreeNode(t.Item1, t.Item2)))
            };

            yield return new TextTreeNode("Placement Parameters")
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Ground Point", this.Item.GroundPoint?.ToString() ?? NotAvailable),
                    new TextTreeNode("Painter Ground Margin", this.Item.PainterGroundMargin?.ToString() ?? NotAvailable),
                    new TextTreeNode("Orbital Center Height from Ground", this.Item.OrbitalCenterHeightFromGround?.ToString() ?? NotAvailable),
                    new TextTreeNode("Orbital Radius Base", this.Item.OrbitalRadiusBase?.ToString() ?? NotAvailable),
                    new TextTreeNode("Orbital Preview Angle", this.Item.OrbitalPreviewAngle?.ToString() ?? NotAvailable),
                }
            };

            yield return new TextTreeNode("Product State", this.Item.ProductState?.ToString() ?? NotAvailable);

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
