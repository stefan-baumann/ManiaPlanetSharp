using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class MetadataProvider
    {
        public MetadataProvider(Stream stream)
            : this(new GameBoxFileParser(stream).Parse())
        { }

        public MetadataProvider(GameBoxFile file)
        {
            this.ParsedFile = file;
            this.headerNodes = this.ParsedFile.Header
                .Where(node => !(new[] { typeof(Node), typeof(UnusedClass) }).Contains(node.GetType()))
                .Select(node => Tuple.Create(node.GetType(), node))
                .Distinct(new NodeTypeComparer())
                .ToDictionary(node => node.Item1, node => node.Item2);
            this.bodyNodes = this.ParsedFile.Body
                .Where(node => !(new[] { typeof(Node), typeof(UnusedClass) }).Contains(node.GetType()))
                .Select(node => Tuple.Create(node.GetType(), node))
                .Distinct(new NodeTypeComparer())
                .ToDictionary(node => node.Item1, node => node.Item2);
        }

        private class NodeTypeComparer
            : IEqualityComparer<Tuple<Type, Node>>
        {
            bool IEqualityComparer<Tuple<Type, Node>>.Equals(Tuple<Type, Node> x, Tuple<Type, Node> y)
            {
                return x?.Item1 == y?.Item1;
            }

            int IEqualityComparer<Tuple<Type, Node>>.GetHashCode(Tuple<Type, Node> obj)
            {
                return obj?.Item1.GetType().GetHashCode() ?? 0;
            }
        }

        protected GameBoxFile ParsedFile { get; private set; }

        private Dictionary<Type, Node> headerNodes = new Dictionary<Type, Node>();
        protected TClass GetHeaderNode<TClass>()
            where TClass : Node
        {
            if (this.headerNodes.ContainsKey(typeof(TClass)))
            {
                return (TClass)this.headerNodes[typeof(TClass)];
            }
            return null;
        }

        private Dictionary<Type, Node> bodyNodes = new Dictionary<Type, Node>();
        protected TClass GetBodyNode<TClass>()
            where TClass : Node
        {
            if (this.bodyNodes.ContainsKey(typeof(TClass)))
            {
                return (TClass)this.bodyNodes[typeof(TClass)];
            }
            return null;
        }
    }
}
