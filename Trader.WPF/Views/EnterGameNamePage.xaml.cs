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
using System.Windows.Shapes;
using Trader.WPF.Helpers;
using Trader.WPF.ViewModels;
using WPF.Common.Infrastructure;

namespace Trader.WPF.Views
{
    /// <summary>
    /// Interaction logic for EnterGameNameWindow.xaml
    /// </summary>
    public partial class EnterGameNameWindow : Page
    {
        public EnterGameNameWindow()
        {
            InitializeComponent();
            this.DataContext = new EnterGameNamePageViewModel(ApplicationService.Instance.EventAggregator);
        }
    }
}
