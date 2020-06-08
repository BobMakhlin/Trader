using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Windows;
using Trader.Logging.Helpers;
using Trader.WPF.ViewModels;
using Trader.WPF.ViewModels.DialogWindows;
using Trader.WPF.Views;
using Trader.WPF.Views.DialogWindows;

namespace Trader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            try
            {
                var mainWindow = new MainWindow();

                LoggingHelper.Instance.Info("The program launching succeeded");

                return mainWindow;
            }
            catch (Exception ex)
            {
                LoggingHelper.Instance.Fatal("The program launching failed", ex);
                throw;
            }
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageBoxOk, MessageBoxOkViewModel>("MessageBoxOk");
            containerRegistry.RegisterDialog<MessageBoxYesNo, MessageBoxYesNoViewModel>("MessageBoxYesNo");
        }
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            // Pass the ViewModels into Views.
            ViewModelLocationProvider.Register<MessageBoxOk, MessageBoxOkViewModel>();
            ViewModelLocationProvider.Register<MessageBoxYesNo, MessageBoxYesNoViewModel>();
        }
    }
}
