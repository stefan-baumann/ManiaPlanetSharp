namespace ManiaPlanetSharp.GameBoxView
{
    public class PlayerTreeNode
        : FormattedTextTreeNode
    {
        public PlayerTreeNode(string name, string nickname, string login, string zone)
            : base(name, nickname)
        {
            this.Nickname = nickname;
            this.Login = login;
            this.Zone = zone;

            this.HideValueWhenExpanded = true;

            if (!string.IsNullOrWhiteSpace(this.Nickname))
                this.Nodes.Add(new FormattedTextTreeNode("Nickname", this.Nickname));
            if (!string.IsNullOrWhiteSpace(this.Login))
                this.Nodes.Add(new TextTreeNode("Login", this.Login));
            if (!string.IsNullOrWhiteSpace(this.Zone))
                this.Nodes.Add(new TextTreeNode("Zone", this.Zone));
        }

        public string Nickname { get; private set; }
        public string Login { get; private set; }
        public string Zone { get; private set; }

        public override string FormattedText => this.Nickname ?? "None";
    }
}
