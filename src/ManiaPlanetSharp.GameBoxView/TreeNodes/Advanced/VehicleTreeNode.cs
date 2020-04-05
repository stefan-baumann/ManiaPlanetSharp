namespace ManiaPlanetSharp.GameBoxView
{
    public class VehicleTreeNode
        : TextTreeNode
    {
        public VehicleTreeNode(string name, string vehicle, string author, string collection)
            : base(name)
        {
            this.Vehicle = vehicle;
            this.Author = author;
            this.Collection = collection;

            if (!string.IsNullOrWhiteSpace(this.Vehicle))
            {
                this.HideValueWhenExpanded = true;
                this.Nodes.Add(new TextTreeNode("Name", this.Vehicle));
                if (!string.IsNullOrWhiteSpace(this.Author))
                    this.Nodes.Add(new TextTreeNode("Author", this.Author));
                if (!string.IsNullOrWhiteSpace(this.Collection))
                    this.Nodes.Add(new TextTreeNode("Collection", this.Collection));
            }
        }

        public virtual string Vehicle { get; private set; }
        public virtual string Author { get; private set; }
        public virtual string Collection { get; private set; }

        public override string Value => !string.IsNullOrWhiteSpace(this.Vehicle) ? this.Vehicle : "Default";
    }
}
