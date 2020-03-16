using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.Utilities
{
    public class IndentingStringBuilder
    {
        public IndentingStringBuilder(string indentation = "    ")
        {
            this.Indentation = indentation;
        }

        public string Indentation { get; protected set; }

        protected StringBuilder Builder { get; private set; } = new StringBuilder();

        public int IndentationLevel { get; set; } = 0;

        public void AppendLine(string value)
        {
            for (int i = 0; i < this.IndentationLevel; i++)
            {
                this.Builder.Append(this.Indentation);
            }
            this.Builder.AppendLine(value);
        }

        public override string ToString()
        {
            return this.Builder.ToString();
        }

        public void Indent()
        {
            this.IndentationLevel++;
        }

        public void UnIndent()
        {
            if (this.IndentationLevel <= 0)
            {
                throw new InvalidOperationException();
            }
            this.IndentationLevel--;
        }
    }
}
