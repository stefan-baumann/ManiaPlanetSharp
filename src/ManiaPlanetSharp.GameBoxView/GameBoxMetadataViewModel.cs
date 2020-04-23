using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using ManiaPlanetSharp.GameBox.Parsing;
using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            this.MetadataTreeItems.Add(new FileMetadataTreeNode(this.File));

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
            else if (path.ToLowerInvariant().EndsWith(".macroblock.gbx"))
            {
                this.FileType = "Macroblock";
                this.MetadataProvider = new MacroblockMetadataProvider(this.File);
            }

            if (this.MetadataProvider != null)
            {
                if (this.MetadataProvider is MapMetadataProvider map)
                {
                    this.MetadataTreeItems.Add(new MapMetadataTreeNode(map) { IsExpanded = true });
                }
                else if (this.MetadataProvider is ItemMetadataProvider item)
                {
                    this.MetadataTreeItems.Add(new ItemMetadataTreeNode(item) { IsExpanded = true });
                }
                else if (this.MetadataProvider is MacroblockMetadataProvider macroblock)
                {
                    this.MetadataTreeItems.Add(new MacroblockMetadataTreeNode(macroblock) { IsExpanded = true });
                }

                //this.OnPropertyChanged(nameof(this.MetadataTreeItems));
            }
        }



        public string Path { get; private set; }

        public string FileType { get; private set; }

        public GameBoxFile File { get; private set; }

        public MetadataProvider MetadataProvider { get; private set; }



        public string TitleText => $"MP# GameBoxView - {(string.IsNullOrWhiteSpace(this.Path) ? "drag and drop file to open" : this.Path)}";

        public ObservableCollection<TextTreeNode> MetadataTreeItems { get; private set; } = new ObservableCollection<TextTreeNode>();



        public event PropertyChangedEventHandler PropertyChanged;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
