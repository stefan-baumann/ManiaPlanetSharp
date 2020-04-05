using System;

namespace ManiaPlanetSharp.GameBoxView
{
    public class TimesTreeNode
        : TimeTreeNode
    {
        public TimesTreeNode(string name, TimeSpan? author, int? score, TimeSpan? gold, TimeSpan? silver, TimeSpan? bronze)
            : base(name, author)
        {
            this.Author = author;
            this.Gold = gold;
            this.Silver = silver;
            this.Bronze = bronze;

            this.HideValueWhenExpanded = true;

            this.Nodes.Add(new TimeTreeNode("Author Time", this.Author));
            this.Nodes.Add(new TextTreeNode("Author Score", $"{this.Score?.ToString() ?? "no"} points"));
            this.Nodes.Add(new TimeTreeNode("Gold Time", this.Gold));
            this.Nodes.Add(new TimeTreeNode("Silver Time", this.Silver));
            this.Nodes.Add(new TimeTreeNode("Bronze Time", this.Bronze));
        }

        public virtual TimeSpan? Author { get; private set; }
        public virtual int? Score { get; private set; }
        public virtual TimeSpan? Gold { get; private set; }
        public virtual TimeSpan? Silver { get; private set; }
        public virtual TimeSpan? Bronze { get; private set; }

        public override TimeSpan? Time => this.Author;


        public override string Value => $"{base.Value} ({this.Score?.ToString() ?? "no"} points)";
    }
}
