using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trader.WPF.Helpers;
using Trader.WPF.ViewModels;
using WPF.Common.Services;

namespace Trader.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoadGamePage.xaml
    /// </summary>
    public partial class LoadGamePage : Page
    {
        public LoadGamePage()
        {
            InitializeComponent();
            this.DataContext = new LoadGamePageViewModel
            (
                new DialogService(), 
                ApplicationService.Instance.EventAggregator
            );
        }
    }
}
