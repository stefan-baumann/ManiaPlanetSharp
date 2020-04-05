using System;
using System.Windows.Input;

namespace ManiaPlanetSharp.GameBoxView
{
    public class ButtonTreeNode
        : TextTreeNode
    {
        public ButtonTreeNode(string name, string value)
            : base(name, value)
        { }

        private ICommand buttonClickCommand;

        public ICommand ButtonClickCommand => this.buttonClickCommand ?? (this.buttonClickCommand = new RelayCommand(_ => this.OnButtonClicked()));

        public event EventHandler ButtonClicked;
        public virtual void OnButtonClicked()
        {
            this.ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
