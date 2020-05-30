using GalaSoft.MvvmLight.Command;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.WPF.Helpers;
using Trader.WPF.Models;
using WPF.Common.Helpers;

namespace Trader.WPF.ViewModels
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Private Definitions
        string m_currentPagePath;
        bool m_menuOpened;
        IEventAggregator m_eventAggregator;

        #endregion

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            InitCommands();

            m_eventAggregator = eventAggregator;
            m_eventAggregator.GetEvent<GameIdEvent>().Subscribe(OnGameIdReceived);

            CurrentPagePath = GamePagesList.EnterGameNamePagePath;
        }

        public string CurrentPagePath
        {
            get => m_currentPagePath;
            set
            {
                m_currentPagePath = value;
                RaisePropertyChanged();
            }
        }
        public bool IsMenuOpened
        {
            get => m_menuOpened;
            set
            {
                m_menuOpened = value;
                RaisePropertyChanged();
            }
        }

        public ICommand OpenEnterGameNamePageCommand { get; set; }
        public ICommand OpenLoadGamePageCommand { get; set; }

        void InitCommands()
        {
            OpenEnterGameNamePageCommand = new RelayCommand(OpenEnterGameNamePage);
            OpenLoadGamePageCommand = new RelayCommand(OpenLoadGamePage);
        }

        void OpenEnterGameNamePage()
        {
            CurrentPagePath = GamePagesList.EnterGameNamePagePath;
            IsMenuOpened = false;
        }
        void OnGameIdReceived(int gameId)
        {
            MessageBox.Show($"New game id: {gameId}");
        }

        void OpenLoadGamePage()
        {
            CurrentPagePath = GamePagesList.LoadGamePagePath;
            IsMenuOpened = false;
        }
    }
}
