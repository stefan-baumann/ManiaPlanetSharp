using ManiaPlanetSharp.GameBox;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ManiaPlanetSharp.GameBoxView
{
    public class ChunkTreeNode
        : TextTreeNode
    {
        public ChunkTreeNode(string name, Chunk chunk)
            : base(name)
        {
            this.Chunk = chunk;

            if (chunk != null)
            {
                foreach (var property in chunk.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(property => property.DeclaringType == chunk.GetType()))
                {
                    if (property.PropertyType.GetCustomAttributes(false).Any(a => a.GetType().Name == "XmlRootAttribute"))
                    {
                        var value = property.GetValue(chunk);
                        if (value != null)
                        {
                            this.Nodes.Add(new TextTreeNode(property.Name)
                            {
                                Nodes = new ObservableCollection<TextTreeNode>(this.DeconstructXmlProperty(value))
                            });
                        }
                        else
                        {
                            this.Nodes.Add(new TextTreeNode(property.Name, "null"));
                        }
                    }
                    else
                    {
                        var text = property.GetValue(chunk)?.ToString() ?? "null";
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            text = $"\"{text}\"";
                        }
                        this.Nodes.Add(new TextTreeNode(property.Name, text));
                    }
                }
            }
        }

        private IEnumerable<TextTreeNode> DeconstructXmlProperty(object value)
        {
            foreach (var property in value.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(property => property.DeclaringType == value.GetType()))
            {
                if (property.PropertyType.GetCustomAttributes(false).Any(a => a.GetType().Name == "XmlRootAttribute"))
                {
                    var propertyValue = property.GetValue(value);
                    if (propertyValue != null)
                    {
                        yield return new TextTreeNode(property.Name)
                        {
                            Nodes = new ObservableCollection<TextTreeNode>(this.DeconstructXmlProperty(propertyValue))
                        };
                    }
                    else
                    {
                        yield return new TextTreeNode(property.Name, "null");
                    }
                }
                else
                {
                    var text = property.GetValue(value)?.ToString() ?? "null";
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        text = $"\"{text}\"";
                    }
                    yield return new TextTreeNode(property.Name, text);
                }
            }

            yield break;
        }

        public virtual Chunk Chunk { get; private set; }
    }
}
