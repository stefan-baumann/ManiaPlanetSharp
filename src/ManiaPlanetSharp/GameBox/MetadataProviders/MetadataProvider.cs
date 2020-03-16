using ManiaPlanetSharp.GameBox.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public abstract class MetadataProvider
    {
        protected MetadataProvider(GameBoxFile file)
        {
            this.File = file;

            this.headerNodes = this.File.HeaderChunks
                .Where(node => node.GetType() != typeof(UnknownChunk))
                .GroupBy(node => node.GetType())
                .ToDictionary(group => group.Key, group => group.ToArray());
        }

        public GameBoxFile File { get; set; }

        private Dictionary<Type, Node[]> headerNodes = new Dictionary<Type, Node[]>();
        protected TChunk[] GetHeaderNodes<TChunk>()
            where TChunk : Node
        {
            if (this.headerNodes.ContainsKey(typeof(TChunk)))
            {
                return this.headerNodes[typeof(TChunk)].OfType<TChunk>().ToArray();
            }
            return null;
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
    }
}
