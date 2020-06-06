using System;
using System.Windows;
using Trader.Logging.Helpers;
using Trader.WPF.ViewModels;
using Trader.WPF.Views;

namespace Trader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
