using System;
using System.Text.RegularExpressions;

namespace ManiaPlanetSharp.GameBoxView
{
    public class TimeTreeNode
        : TextTreeNode
    {
        public TimeTreeNode(string name, TimeSpan? time) 
            : base(name)
        {
            this.Time = time;
        }

        public virtual TimeSpan? Time { get; private set; }

        public override string Value => Regex.Replace(this.Time?.ToString() ?? "None", @"(^(00:){0,2})|((?<=\d{3})0+$)", "");
    }
}
