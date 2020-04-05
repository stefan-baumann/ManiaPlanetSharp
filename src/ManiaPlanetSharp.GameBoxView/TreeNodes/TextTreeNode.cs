using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ManiaPlanetSharp.GameBoxView
{
    public class TextTreeNode
        : INotifyPropertyChanged
    {
        public TextTreeNode(string name)
        {
            this.Name = name;
        }

        public TextTreeNode(string name, string value)
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



        public virtual ObservableCollection<TextTreeNode> Nodes { get; set; } = new ObservableCollection<TextTreeNode>();

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
}
