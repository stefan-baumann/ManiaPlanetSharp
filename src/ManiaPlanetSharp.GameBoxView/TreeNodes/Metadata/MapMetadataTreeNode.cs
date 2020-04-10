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
    public class MapMetadataTreeNode
        : MetadataTreeNode
    {
        private const string NotAvailable = "not available";

        public MapMetadataTreeNode(MapMetadataProvider map) : base("Map Metadata")
        {
            this.Map = map;
            this.InitializeNodes();
        }

        public MapMetadataProvider Map { get; private set; }

        protected override IEnumerable<TextTreeNode> GetNodes()
        {
            yield return new FormattedTextTreeNode("Name", Map.Name);
            yield return new FormattedTextTreeNode("Comment", string.IsNullOrWhiteSpace(Map.Comment) ? "no comment" : Map.Comment);
            yield return new PlayerTreeNode("Author", Map.AuthorNickname, Map.Author, Map.AuthorZone);
            yield return new TextTreeNode("Uid", Map.Uid)
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Uid", Map.Uid),
                    new TextTreeNode("Lightmap Cache Uid", Map.LightmapCacheUid != null ? Map.LightmapCacheUid.Value.ToString() : NotAvailable)
                }
            };
            yield return new TextTreeNode("Titlepack", Map.Titlepack);
            yield return new TextTreeNode("Environment", Map.Environment);
            yield return new VehicleTreeNode("Vehicle", Map.Vehicle, Map.VehicleAuthor, Map.VehicleCollection);
            yield return new TextTreeNode("Mod", string.IsNullOrWhiteSpace(Map.Mod) ? "no mod" : Map.Mod);

            yield return new TextTreeNode("Thumbnail") { Nodes = new ObservableCollection<TextTreeNode>() { new ImageTreeNode(this.ImageSourceFromImage(Map.GenerateThumbnailImage()), new Size(256, 256)) } };

            yield return new TimesTreeNode("Author Time", Map.AuthorTime, Map.AuthorScore, Map.GoldTime, Map.SilverTime, Map.BronzeTime);

            yield return new TextTreeNode("Checkpoints", Map.Checkpoints?.ToString() ?? NotAvailable);
            yield return new TextTreeNode("Multilap", Map.IsMultilap != null ? Map.IsMultilap.Value ? $"True ({Map.Laps ?? 0} lap{(Map.Laps == 1 ? "" : "s")})" : "False" : NotAvailable)
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Is Multilap", Map.IsMultilap != null ? Map.IsMultilap.Value ? "True" : "False" : NotAvailable),
                    new TextTreeNode("Lap Count", Map.Laps?.ToString() ?? NotAvailable),
                }
            };

            yield return new FileReferenceTreeNode("Custom Music", Map.CustomMusic);
            yield return new TextTreeNode("Dependencies", $"{Map.Dependencies?.Length ?? 0} dependencies")
            {
                Nodes = new ObservableCollection<TextTreeNode>((Map.Dependencies ?? Array.Empty<MapCommunityDependency>()).Select(d => new DependencyTreeNode(d)))
            };

            yield return new TextTreeNode("Display Cost", Map.DisplayCost?.ToString() ?? NotAvailable);
            yield return new TextTreeNode("Validated", Map.Validated != null ? Map.Validated.Value ? "True" : "False" : NotAvailable);

            yield return new DecorationTreeNode("Decoration", Map.TimeOfDay, Map.DecorationEnvironment, Map.DecorationEnvironmentAuthor, Map.Size);

            var embeddedItemFiles = Map.GetEmbeddedItemFiles();
            yield return new TextTreeNode("Embedded Items", $"{Map.EmbeddedItems?.Length ?? 0} embedded item{(Map.EmbeddedItems?.Length == 1 ? "" : "s")}")
            {
                Nodes = new ObservableCollection<TextTreeNode>((Map.EmbeddedItems ?? Array.Empty<EmbeddedItem>()).Select(i => new EmbeddedItemTreeNode(i, embeddedItemFiles.FirstOrDefault(f => f.Path.Replace('/', '\\').EndsWith(i.Path)))))
            };

            yield return new TextTreeNode("Other")
            {
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Locked", Map.Locked != null ? Map.Locked.Value ? "True" : "False" : NotAvailable),
                    new TextTreeNode("Uses Advanced Editor", Map.UsesAdvancedEditor != null ? Map.UsesAdvancedEditor.Value ? "True" : "False" : NotAvailable),
                    new TextTreeNode("Has Ghost blocks", Map.HasGhostBlocks != null ? Map.HasGhostBlocks.Value ? "True" : "False" : NotAvailable),
                    new TextTreeNode("Origin", Map.Origin != null ? $"{{ X = {Map.Origin.Value.X}, Y = {Map.Origin.Value.Y} }}" : NotAvailable),
                    new TextTreeNode("Target", Map.Target != null ? $"{{ X = {Map.Target.Value.X}, Y = {Map.Target.Value.Y} }}" : NotAvailable),
                }
            };

            yield return new TextTreeNode("Version", Map.ExecutableVersion ?? NotAvailable)
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Executable Version", Map.ExecutableVersion ?? NotAvailable),
                    new TextTreeNode("Executable Build Date", Map.ExecutableBuildDate ?? NotAvailable),
                    new TextTreeNode("Lightmap Version", Map.LightmapVersion?.ToString() ?? NotAvailable)
                }
            };

            yield break;
        }

        protected ImageSource ImageSourceFromImage(System.Drawing.Image source)
        {
            if (source == null)
            {
                return null;
            }
            using (var ms = new MemoryStream())
            {
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
}
