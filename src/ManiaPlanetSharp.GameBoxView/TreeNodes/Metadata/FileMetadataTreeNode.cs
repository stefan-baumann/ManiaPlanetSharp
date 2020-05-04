using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBoxView
{
    public class FileMetadataTreeNode
        : MetadataTreeNode
    {
        public FileMetadataTreeNode(GameBoxFile file)
            : base("GameBox File")
        {
            this.File = file;
            this.InitializeNodes();
        }

        public GameBoxFile File { get; private set; }

        protected override IEnumerable<TextTreeNode> GetNodes()
        {
            yield return new TextTreeNode("Version", File.Version.ToString());
            yield return new TextTreeNode("Main Class", ClassIds.GetClassName(File.MainClass))
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Name", ClassIds.GetClassName(File.MainClass)),
                    new TextTreeNode("ID", $"0x{File.MainClassId:X8}"),
                }
            };
            yield return new TextTreeNode("File Format", File.FileFormat.ToString())
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Format Type", File.FileFormat.ToString()),
                    new TextTreeNode("Reference Table Compressed", File.ReferenceTableCompressed ? "True" : "False"),
                    new TextTreeNode("Body Compressed", File.BodyCompressed ? "True" : "False"),
                }
            };

            yield return new TextTreeNode("Header", $"{File.HeaderChunkCount} chunks")
            {
                Nodes = new ObservableCollection<TextTreeNode>(File.HeaderChunkEntries.Select(he => (he, chunk: (Chunk)File.HeaderChunks.FirstOrDefault(n => n.Id == he.ChunkID))).Select((c, i) =>
                {
                    var chunkType = ParserFactory.TryGetChunkParser(c.he.ChunkID, out IChunkParser<Chunk> parser) ? parser.GetType().GetInterfaces().FirstOrDefault(t => t.GetGenericTypeDefinition() == typeof(IChunkParser<>))?.GetGenericArguments().FirstOrDefault()?.Name : "Unknown Chunk";
                    return new TextTreeNode($"{chunkType}", $"0x{c.he.ChunkID:X8} ({ ClassIds.GetClassName(c.he.ChunkID & ~0xFFFU)})")
                    {
                        Nodes = new ObservableCollection<TextTreeNode>()
                        {
                            new TextTreeNode("Size", $"{c.he.ChunkSize} bytes"),
                            new TextTreeNode("Heavy Chunk", c.he.IsHeavyChunk ? "True" : "False"),
                            new ChunkTreeNode("Extracted Chunk Data", c.chunk)
                        }
                    };
                }))
            };

            yield return new TextTreeNode("Reference Table", $"{File.ReferenceTableExternalNodeCount} external nodes")
            {
                Nodes = new ObservableCollection<TextTreeNode>()
                {
                    new TextTreeNode("Ancestor Level", File.ReferenceTableAncestorLevel.ToString()),
                    new TextTreeNode("Folders", $"{File.ReferenceTableFolders?.Length ?? 0} folders")
                    {
                        Nodes = new ObservableCollection<TextTreeNode>((File.ReferenceTableFolders ?? Array.Empty<ReferenceTableFolder>()).Select(rtf => new ReferenceTableFolderTreeNode(rtf)))
                    },
                    new TextTreeNode("External Nodes", $"{File.ReferenceTableExternalNodeCount} nodes")
                    {
                        Nodes = new ObservableCollection<TextTreeNode>((File.ReferenceTableExternalNodes ?? Array.Empty<ReferenceTableExternalNode>()).Select(rten => new ReferenceTableTreeNode(rten)))
                    }
                }
            };

            var bodyNodes = this.File.ParseBody().ToList();
            yield return new TextTreeNode("Body", $"{bodyNodes.Count} chunks")
            {
                Nodes = new ObservableCollection<TextTreeNode>(bodyNodes.Select(c => new ChunkTreeNode(c.ToString(), (Chunk)c)))
            };

            yield break;
        }
    }
}
