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
            try
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();

                LoggingHelper.Instance.Info("The program launching succeeded");
            }
            catch (Exception ex)
            {
                LoggingHelper.Instance.Fatal("The program launching failed", ex);
            }
        }
    }
}
