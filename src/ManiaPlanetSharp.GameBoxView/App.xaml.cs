using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ManiaPlanetSharp.GameBoxView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args?.Length == 1)
            {
                string path = e.Args[0];
                if (File.Exists(path) && path.ToLowerInvariant().EndsWith(".gbx"))
                {
                    this.MainWindow = new MainWindow(new GameBoxMetadataViewModel(path));
                }
            }

            this.MainWindow = this.MainWindow ?? new MainWindow();
            this.MainWindow.Show();
        }
    }
}
