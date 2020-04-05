using ManiaPlanetSharp.GameBox;
using System.Collections.ObjectModel;

namespace ManiaPlanetSharp.GameBoxView
{
    public class DecorationTreeNode
        : TextTreeNode
    {
        public DecorationTreeNode(string name, string timeOfDay, string environment, string environmentAuthor, Size3D? size)
            : base(name, $"{timeOfDay} ({environment})")
        {
            this.TimeOfDay = timeOfDay;
            this.Environment = environment;
            this.EnvironmentAuthor = environmentAuthor;
            this.Size = size;

            this.HideValueWhenExpanded = true;

            if (!string.IsNullOrWhiteSpace(this.TimeOfDay))
                this.Nodes.Add(new TextTreeNode("Time of day", this.TimeOfDay));
            if (this.Size != null)
                this.Nodes.Add(new TextTreeNode("Size", $"{this.Size.Value.X}x{this.Size.Value.Y}x{this.Size.Value.Z}") { HideValueWhenExpanded = true, Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("X", this.Size.Value.X.ToString()),
                    new TextTreeNode("Y", this.Size.Value.Y.ToString()),
                    new TextTreeNode("Z", this.Size.Value.Z.ToString())
                } });
            if (!string.IsNullOrWhiteSpace(this.Environment))
                this.Nodes.Add(new TextTreeNode("Environment", this.Environment));
            if (!string.IsNullOrWhiteSpace(this.EnvironmentAuthor))
                this.Nodes.Add(new TextTreeNode("Environment Author", this.EnvironmentAuthor));
        }

        public string TimeOfDay { get; private set; }
        public string Environment { get; private set; }
        public string EnvironmentAuthor { get; private set; }
        public Size3D? Size { get; private set; }
    }
}
