using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ManiaPlanetSharp.GameBoxView
{
    public class GameBoxMetadataViewModel
        : INotifyPropertyChanged
    {
        private const string NotAvailable = "not available";

        public GameBoxMetadataViewModel()
        { }

        public GameBoxMetadataViewModel(string path)
            : this()
        {
            this.Path = path;
            this.File = GameBoxFile.Parse(this.Path);

            if (path.ToLowerInvariant().EndsWith(".map.gbx"))
            {
                this.FileType = "Map";
                this.MetadataProvider = new MapMetadataProvider(this.File);
            }
            else if (path.ToLowerInvariant().EndsWith(".item.gbx"))
            {
                this.FileType = "Item";
                this.MetadataProvider = new ItemMetadataProvider(this.File);
            }
            else if (path.ToLowerInvariant().EndsWith(".block.gbx"))
            {
                this.FileType = "Block";
                this.MetadataProvider = new ItemMetadataProvider(this.File);
            }

            if (this.MetadataProvider != null)
            {
                this.MetadataTreeItems.Add(new MetadataTreeNode("GameBox File") { Nodes = new ObservableCollection<MetadataTreeNode>(this.GetFileMetadataTreeNodes(this.File)) });
                if (this.MetadataProvider is MapMetadataProvider map)
                {
                    this.MetadataTreeItems.Add(new MetadataTreeNode("Map Metadata", "") { IsExpanded = true, Nodes = new ObservableCollection<MetadataTreeNode>(this.GetMapMetadataTreeNodes(map)) });
                }
                //foreach (var property in this.MetadataProvider.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(property => property.Name != "File"))
                //{
                //    this.MetadataTreeItems.Add(this.GetMetadataPropertyNode(property.Name, property.GetValue(this.MetadataProvider)));
                //}

                this.OnPropertyChanged(nameof(this.MetadataTreeItems));
            }
        }

        protected MetadataTreeNode GetMetadataPropertyNode(string name, object value)
        {
            if (value is Dependency[] dependencies)
            {
                return new MetadataTreeNode(name, $"{dependencies.Length} dependencies") { Nodes = new ObservableCollection<MetadataTreeNode>(dependencies.Select(d => new MetadataTreeNode(d.File, d.Url ?? "local"))) };
            } else if (value is FileReference reference)
            {
                return new MetadataTreeNode(name, System.IO.Path.GetFileName(reference.FilePath)) { Nodes = new ObservableCollection<MetadataTreeNode>(new[]
                {
                    new MetadataTreeNode("Path", $"{reference.FilePath}{(reference.IsRelativePath ? " (relative)" : "")}"),
                    new MetadataTreeNode("Url", reference.LocatorUrl),
                    new MetadataTreeNode("Checksum", string.Join("", reference.Checksum.Select(b => b.ToString("X2"))))
                }) };
            } else if (name == "Name" || name == "AuthorNickname")
            {
                return new FormattedTextTreeNode(name, (string)value);
            }

            return new MetadataTreeNode(name, value?.ToString() ?? "null");
        }

        protected IEnumerable<MetadataTreeNode> GetMapMetadataTreeNodes(MapMetadataProvider map)
        {
            yield return new FormattedTextTreeNode("Name", map.Name);
            yield return new FormattedTextTreeNode("Comment", string.IsNullOrWhiteSpace(map.Comment) ? "no comment" : map.Comment);
            yield return new PlayerTreeNode("Author", map.AuthorNickname, map.Author, map.AuthorZone);
            yield return new MetadataTreeNode("Uid", map.Uid)
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<MetadataTreeNode>()
                {
                    new MetadataTreeNode("Uid", map.Uid),
                    new MetadataTreeNode("Lightmap Cache Uid", map.LightmapCacheUid != null ? map.LightmapCacheUid.Value.ToString() : NotAvailable)
                }
            };
            yield return new MetadataTreeNode("Titlepack", map.Titlepack);
            yield return new MetadataTreeNode("Environment", map.Environment);
            yield return new VehicleTreeNode("Vehicle", map.Vehicle, map.VehicleAuthor, map.VehicleCollection);
            yield return new MetadataTreeNode("Mod", string.IsNullOrWhiteSpace(map.Mod) ? "no mod" : map.Mod);

            yield return new TimesTreeNode("Author Time", map.AuthorTime, map.AuthorScore, map.GoldTime, map.SilverTime, map.BronzeTime);

            yield return new MetadataTreeNode("Checkpoints", map.Checkpoints?.ToString() ?? NotAvailable);
            yield return new MetadataTreeNode("Multilap", map.IsMultilap != null ? map.IsMultilap.Value ? $"True ({map.Laps ?? 0} lap{(map.Laps == 1 ? "" : "s")})" : "False" : NotAvailable)
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<MetadataTreeNode>()
                {
                    new MetadataTreeNode("Is Multilap", map.IsMultilap != null ? map.IsMultilap.Value ? "True" : "False" : NotAvailable),
                    new MetadataTreeNode("Lap Count", map.Laps?.ToString() ?? NotAvailable),
                }
            };

            yield return new FileReferenceTreeNode("Custom Music", map.CustomMusic);
            yield return new MetadataTreeNode("Dependencies", $"{map.Dependencies?.Length ?? 0} dependencies")
            {
                Nodes = new ObservableCollection<MetadataTreeNode>((map.Dependencies ?? Array.Empty<Dependency>()).Select(d => new DependencyTreeNode(d)))
            };
            yield return new DecorationTreeNode("Decoration", map.TimeOfDay, map.DecorationEnvironment, map.DecorationEnvironmentAuthor, map.Size);

            yield return new MetadataTreeNode("Version", map.ExecutableVersion ?? NotAvailable)
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<MetadataTreeNode>()
                {
                    new MetadataTreeNode("Executable Version", map.ExecutableVersion ?? NotAvailable),
                    new MetadataTreeNode("Executable Build Date", map.ExecutableBuildDate ?? NotAvailable),
                    new MetadataTreeNode("Lightmap Version", map.LightmapVersion?.ToString() ?? NotAvailable)
                }
            };

            yield break;
        }

        protected IEnumerable<MetadataTreeNode> GetFileMetadataTreeNodes(GameBoxFile file)
        {
            yield return new MetadataTreeNode("Version", file.Version.ToString());
            yield return new MetadataTreeNode("Main Class ID", $"0x{file.MainClassID:X8} ({KnownClassIds.GetClassName(file.MainClassID & ~0xFFFU)})");
            yield return new MetadataTreeNode("File Format", file.FileFormat.ToString())
            {
                HideValueWhenExpanded = true,
                Nodes = new ObservableCollection<MetadataTreeNode>()
                {
                    new MetadataTreeNode("Format Type", file.FileFormat.ToString()),
                    new MetadataTreeNode("Reference Table Compressed", file.ReferenceTableCompressed ? "True" : "False"),
                    new MetadataTreeNode("Body Compressed", file.BodyCompressed ? "True" : "False"),
                }
            };

            yield return new MetadataTreeNode("Header", $"{file.HeaderChunkCount} chunks")
            {
                Nodes = new ObservableCollection<MetadataTreeNode>(file.HeaderChunkEntries.Select(he => (he, chunk: (Chunk)file.HeaderChunks.FirstOrDefault(n => n.Id == he.ChunkID))).Select((c, i) =>
                {
                    var chunkType = ParserFactory.TryGetChunkParser(c.he.ChunkID, out IChunkParser<Chunk> parser) ? parser.GetType().GetInterfaces().FirstOrDefault(t => t.GetGenericTypeDefinition() == typeof(IChunkParser<>))?.GetGenericArguments().FirstOrDefault()?.Name : "Unknown Chunk";
                    return new MetadataTreeNode($"{chunkType}", $"0x{c.he.ChunkID:X8} ({ KnownClassIds.GetClassName(c.he.ChunkID & ~0xFFFU)})")
                    {
                        Nodes = new ObservableCollection<MetadataTreeNode>()
                        {
                            new MetadataTreeNode("Size", $"{c.he.ChunkSize} bytes"),
                            new MetadataTreeNode("Heavy Chunk", c.he.IsHeavyChunk ? "True" : "False"),
                            new ChunkTreeNode("Extracted Chunk Data", c.chunk)
                        }
                    };
                }))
            };

            yield return new MetadataTreeNode("Reference Table", $"{file.ReferenceTableExternalNodeCount} external nodes")
            {
                Nodes = new ObservableCollection<MetadataTreeNode>()
                {
                    new MetadataTreeNode("Ancestor Level", file.ReferenceTableAncestorLevel.ToString()),
                    new MetadataTreeNode("Folders", $"{file.ReferenceTableFolders?.Length ?? 0} folders")
                    {
                        Nodes = new ObservableCollection<MetadataTreeNode>((file.ReferenceTableFolders ?? Array.Empty<ReferenceTableFolder>()).Select(rtf => new ReferenceTableFolderTreeNode(rtf)))
                    },
                    new MetadataTreeNode("External Nodes", $"{file.ReferenceTableExternalNodeCount} nodes")
                    {
                        Nodes = new ObservableCollection<MetadataTreeNode>((file.ReferenceTableExternalNodes ?? Array.Empty<ReferenceTableExternalNode>()).Select(rten => new ReferenceTableTreeNode(rten)))
                    }
                }
            };

            yield break;
        }



        public string Path { get; private set; }

        public string FileType { get; private set; }

        public GameBoxFile File { get; private set; }

        public MetadataProvider MetadataProvider { get; private set; }



        public string TitleText => $"MP# GameBoxView - {(string.IsNullOrWhiteSpace(this.Path) ? "drag and drop file to open" : this.Path)}";

        public ObservableCollection<MetadataTreeNode> MetadataTreeItems { get; private set; } = new ObservableCollection<MetadataTreeNode>();



        public event PropertyChangedEventHandler PropertyChanged;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MetadataTreeNode
        : INotifyPropertyChanged
    {
        public MetadataTreeNode(string name)
        {
            this.Name = name;
        }

        public MetadataTreeNode(string name, string value)
            : this(name)
        {
            this.Value = value;
        }

        public virtual string Name { get; private set; }

        private string value;
        public virtual string Value
        {
            get => this.value;
            private set
            {
                this.value = value;
                this.OnPropertyChanged();
            }
        }

        public string DisplayText => this.HideValueWhenExpanded && this.IsExpanded ? null : this.Value;

        public bool HasValue => !string.IsNullOrWhiteSpace(this.Value);

        private bool hideValueWhenExpanded = false;
        public bool HideValueWhenExpanded
        {
            get { return this.hideValueWhenExpanded; }
            set
            {
                if (value != this.hideValueWhenExpanded)
                {
                    this.hideValueWhenExpanded = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public virtual string Tooltip { get; private set; }

        public bool HasTooltip => !string.IsNullOrWhiteSpace(this.Tooltip);


        private bool isSelected = false;
        public virtual bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private bool isExpanded = false;

        public virtual bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    this.OnPropertyChanged();

                    if (this.HideValueWhenExpanded)
                    {
                        this.OnPropertyChanged("Value");
                        this.OnPropertyChanged("HasValue");
                    }
                }
            }
        }



        public virtual ObservableCollection<MetadataTreeNode> Nodes { get; set; } = new ObservableCollection<MetadataTreeNode>();

        public event PropertyChangedEventHandler PropertyChanged;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "Value")
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayText"));
            }
        }
    }

    public class FormattedTextTreeNode
        : MetadataTreeNode
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

        public override string Tooltip => this.FormattedText;
    }



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
                this.Nodes.Add(new MetadataTreeNode("Login", this.Login));
            if (!string.IsNullOrWhiteSpace(this.Zone))
                this.Nodes.Add(new MetadataTreeNode("Zone", this.Zone));
        }

        public string Nickname { get; private set; }
        public string Login { get; private set; }
        public string Zone { get; private set; }

        public override string FormattedText => this.Nickname ?? "None";
    }

    public class DecorationTreeNode
        : MetadataTreeNode
    {
        public DecorationTreeNode(string name, string timeOfDay, string environment, string environmentAuthor, Size3D? size)
            : base(name, $"{timeOfDay} ({environment})")
        {
            this.TimeOfDay = timeOfDay;
            this.Environment = environment;
            this.EnvironmentAuthor = environmentAuthor;
            this.Size = size;

            this.HideValueWhenExpanded = true;

            if (!string.IsNullOrWhiteSpace(this.TimeOfDay))
                this.Nodes.Add(new MetadataTreeNode("Time of day", this.TimeOfDay));
            if (this.Size != null)
                this.Nodes.Add(new MetadataTreeNode("Size", $"{this.Size.Value.X}x{this.Size.Value.Y}x{this.Size.Value.Z}") { HideValueWhenExpanded = true, Nodes = new ObservableCollection<MetadataTreeNode>()
                {
                    new MetadataTreeNode("X", this.Size.Value.X.ToString()),
                    new MetadataTreeNode("Y", this.Size.Value.Y.ToString()),
                    new MetadataTreeNode("Z", this.Size.Value.Z.ToString())
                } });
            if (!string.IsNullOrWhiteSpace(this.Environment))
                this.Nodes.Add(new MetadataTreeNode("Environment", this.Environment));
            if (!string.IsNullOrWhiteSpace(this.EnvironmentAuthor))
                this.Nodes.Add(new MetadataTreeNode("Environment Author", this.EnvironmentAuthor));
        }

        public string TimeOfDay { get; private set; }
        public string Environment { get; private set; }
        public string EnvironmentAuthor { get; private set; }
        public Size3D? Size { get; private set; }
    }

    public class FileReferenceTreeNode
        : MetadataTreeNode
    {
        public FileReferenceTreeNode(string name, FileReference value)
            : base(name)
        {
            this.Reference = value;

            if (this.Reference != null && !string.IsNullOrWhiteSpace(this.Reference.FilePath))
            {
                this.Nodes.Add(new MetadataTreeNode("Path", $"{this.Reference.FilePath}{(this.Reference.IsRelativePath ? " (relative)" : "")}"));
                this.Nodes.Add(new MetadataTreeNode("Url", this.Reference.LocatorUrl));
                this.Nodes.Add(new MetadataTreeNode("Checksum", string.Join("", this.Reference.Checksum.Select(b => b.ToString("X2")))));
            }
        }

        public FileReference Reference { get; private set; }

        public override string Value => System.IO.Path.GetFileName(this.Reference?.FilePath) ?? "None";
    }

    public class TimeTreeNode
        : MetadataTreeNode
    {
        public TimeTreeNode(string name, TimeSpan? time) 
            : base(name)
        {
            this.Time = time;
        }

        public virtual TimeSpan? Time { get; private set; }

        public override string Value => Regex.Replace(this.Time?.ToString() ?? "None", @"(^(00:){0,2})|((?<=\d{3})0+$)", "");
    }

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
            this.Nodes.Add(new MetadataTreeNode("Author Score", $"{this.Score?.ToString() ?? "no"} points"));
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

    public class ReferenceTableTreeNode
        : MetadataTreeNode
    {
        public ReferenceTableTreeNode(ReferenceTableExternalNode node)
            : base(node.FileName)
        {
            this.Node = node;

            //this.HideValueWhenExpanded = true;

            if (!string.IsNullOrWhiteSpace(this.Node?.FileName))
                new MetadataTreeNode("File Name", this.Node.FileName);
            new MetadataTreeNode("Resource Index", this.Node.ResourceIndex.ToString());
            new MetadataTreeNode("Node Index", this.Node.NodeIndex.ToString());
            new MetadataTreeNode("Folder Index", this.Node.FolderIndex.ToString());
            new MetadataTreeNode("Use File", this.Node.UseFile ? "True" : "False");
        }

        public ReferenceTableExternalNode Node { get; set; }
    }

    public class ReferenceTableFolderTreeNode
        : MetadataTreeNode
    {
        public ReferenceTableFolderTreeNode(ReferenceTableFolder rtf)
            : base(rtf.Name, $"{rtf.SubFolders.Length} subfolders")
        {
            this.ReferenceTableFolder = rtf;
            if (this.ReferenceTableFolder.SubFolders.Length > 0)
            {
                this.Nodes = new ObservableCollection<MetadataTreeNode>(this.ReferenceTableFolder.SubFolders.Select(rtf => new ReferenceTableFolderTreeNode(rtf)));
            }
        }
        public virtual ReferenceTableFolder ReferenceTableFolder { get; private set; }
    }

    public class ChunkTreeNode
        : MetadataTreeNode
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
                            this.Nodes.Add(new MetadataTreeNode(property.Name)
                            {
                                Nodes = new ObservableCollection<MetadataTreeNode>(this.DeconstructXmlProperty(value))
                            });
                        }
                        else
                        {
                            this.Nodes.Add(new MetadataTreeNode(property.Name, "null"));
                        }
                    }
                    else
                    {
                        var text = property.GetValue(chunk)?.ToString() ?? "null";
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            text = $"\"{text}\"";
                        }
                        this.Nodes.Add(new MetadataTreeNode(property.Name, text));
                    }
                }
            }
        }

        private IEnumerable<MetadataTreeNode> DeconstructXmlProperty(object value)
        {
            foreach (var property in value.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(property => property.DeclaringType == value.GetType()))
            {
                if (property.PropertyType.GetCustomAttributes(false).Any(a => a.GetType().Name == "XmlRootAttribute"))
                {
                    var propertyValue = property.GetValue(value);
                    if (propertyValue != null)
                    {
                        yield return new MetadataTreeNode(property.Name)
                        {
                            Nodes = new ObservableCollection<MetadataTreeNode>(this.DeconstructXmlProperty(propertyValue))
                        };
                    }
                    else
                    {
                        yield return new MetadataTreeNode(property.Name, "null");
                    }
                }
                else
                {
                    var text = property.GetValue(value)?.ToString() ?? "null";
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        text = $"\"{text}\"";
                    }
                    yield return new MetadataTreeNode(property.Name, text);
                }
            }

            yield break;
        }

        public virtual Chunk Chunk { get; private set; }
    }

    public class VehicleTreeNode
        : MetadataTreeNode
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
                this.Nodes.Add(new MetadataTreeNode("Name", this.Vehicle));
                if (!string.IsNullOrWhiteSpace(this.Author))
                    this.Nodes.Add(new MetadataTreeNode("Author", this.Author));
                if (!string.IsNullOrWhiteSpace(this.Collection))
                    this.Nodes.Add(new MetadataTreeNode("Collection", this.Collection));
            }
        }

        public virtual string Vehicle { get; private set; }
        public virtual string Author { get; private set; }
        public virtual string Collection { get; private set; }

        public override string Value => !string.IsNullOrWhiteSpace(this.Vehicle) ? this.Vehicle : "Default";
    }

    public class DependencyTreeNode
        : MetadataTreeNode
    {
        public DependencyTreeNode(Dependency dependency)
            : base($"{System.IO.Path.GetFileName(dependency?.File)} ({(string.IsNullOrWhiteSpace(dependency.Url) ? "local" : "remote")})")
        {
            this.Dependency = dependency;
            if (this.Dependency != null)
            {
                this.Nodes.Add(new MetadataTreeNode("Path", dependency.File));
                if (!string.IsNullOrWhiteSpace(dependency.Url))
                    this.Nodes.Add(new MetadataTreeNode("Url", dependency.Url));
            }
        }

        public virtual Dependency Dependency { get; private set; }
    }
}
