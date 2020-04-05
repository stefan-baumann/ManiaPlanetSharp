using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace ManiaPlanetSharp.GameBoxView
{
    public class FormattedTextTreeNode
        : TextTreeNode
    {
        public FormattedTextTreeNode(string name, string formattedText)
            : base(name)
        {
            this.FormattedText = formattedText;
        }

        public virtual string FormattedText { get; private set; }

        public override string Value
        {
            get
            {
                if (this.FormattedText == null)
                {
                    return null;
                }
                string controlCharsRemoved = Regex.Replace(this.FormattedText, @"(\$[0-9a-f]{3})|(\$(?=\$))|(\$[wnmhoitsgzf])", "", RegexOptions.IgnoreCase);
                string linksRemoved = Regex.Replace(controlCharsRemoved, @"\$l(\[.*?\])?", "", RegexOptions.IgnoreCase);
                return linksRemoved;
            }
        }

        public virtual List<TextWithFormat> ValueFormatted
        {
            get
            {
                List<TextWithFormat> parts = new List<TextWithFormat>();
                List<Action<TextWithFormat>> styles = new List<Action<TextWithFormat>>();
                for (string remaining = this.FormattedText; remaining.Length > 0; )
                {
                    if (remaining.StartsWith("$") && !remaining.StartsWith("$$"))
                    {
                        var colorMatch = Regex.Match(remaining, @"^\$([0-9a-f]){3}", RegexOptions.IgnoreCase);
                        if (colorMatch.Success)
                        {
                            var colorString = $"#{colorMatch.Groups[1].Captures[0].Value}{colorMatch.Groups[1].Captures[0].Value}{colorMatch.Groups[1].Captures[1].Value}{colorMatch.Groups[1].Captures[1].Value}{colorMatch.Groups[1].Captures[2].Value}{colorMatch.Groups[1].Captures[2].Value}";
                            styles.Add(t => t.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(colorString));

                            remaining = remaining.Substring(4);
                            continue;
                        }
                        var formatMatch = Regex.Match(remaining, @"^\$([wnmhoitsgzf])");
                        if (formatMatch.Success)
                        {
                            //TODO: Implement modifiers
                            char modifier = formatMatch.Groups[1].Value[0];
                            switch (char.ToLowerInvariant(modifier))
                            {
                                case 'z':
                                    styles.Clear();
                                    break;
                            }

                            remaining = remaining.Substring(2);
                            continue;
                        }
                        var linkMatch = Regex.Match(remaining, @"^\$l(\[.*?\])?");
                        if (linkMatch.Success)
                        {
                            //TODO: Add hyperlink

                            remaining = remaining.Substring(linkMatch.Length);
                            continue;
                        }
                    }
                    var textLength = remaining.Substring(remaining.StartsWith("$$") ? 2 : 1).IndexOf('$') + (remaining.StartsWith("$$") ? 2 : 1);
                    var text = new TextWithFormat(textLength == 0 ? remaining : remaining.Substring(0, textLength));
                    foreach (var style in styles)
                    {
                        style(text);
                    }
                    parts.Add(text);
                    remaining = remaining.Substring(text.Text.Length);
                }
                return parts;
            }
        }

        public override string Tooltip => this.FormattedText;

        public class TextWithFormat
        {
            public TextWithFormat(string text)
            {
                this.Text = text;
            }

            public string Text { get; set; }
            public Brush Foreground { get; set; } = Brushes.White;
        }
    }
}
