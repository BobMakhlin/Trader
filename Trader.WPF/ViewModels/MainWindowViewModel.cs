using System;
using System.Collections.Generic;
using System.Windows.Input;
using Trader.Logging.Helpers;
using Trader.WPF.Infrastructure.MyEventArgs;
using Trader.WPF.ViewModels.PageViewModels.Common;
using Trader.WPF.ViewModels.PageViewModels.Custom;
using WPF.Common.Helpers;
using WPF.Common.Helpers.MyRelayCommand;
using WPF.Common.Services;

/*
    «Нет ничего невозможного. Само слово говорит: „Я возможно!“ (Impossible — I'm possible)» — Одри Хепбёрн. 
*/

namespace Trader.WPF.ViewModels
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Fields
        bool m_menuOpened;
        IPageViewModel m_currentPage;
        Dictionary<Type, IPageViewModel> m_savedViewModels;
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            InitCommands();

            m_savedViewModels = new Dictionary<Type, IPageViewModel>();

            OpenEnterGameNamePage();
        }
        #endregion

        #region Properties
        public ICommand OpenEnterGameNamePageCommand { get; set; }
        public ICommand OpenLoadGamePageCommand { get; set; }
        public ICommand OnWindowClosingCommand { get; set; }

        public bool IsMenuOpened
        {
            get => m_menuOpened;
            set
            {
                m_menuOpened = value;
                RaisePropertyChanged();
            }
        }

        public IPageViewModel CurrentPage
        {
            get => m_currentPage;
            set
            {
                m_currentPage = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Methods
        void InitCommands()
        {
            OpenEnterGameNamePageCommand = new RelayCommand(OpenEnterGameNamePage);
            OpenLoadGamePageCommand = new RelayCommand(OpenLoadGamePage);
            OnWindowClosingCommand = new RelayCommand(OnWindowClosing);
        }

        void OpenEnterGameNamePage()
        {
            IPageViewModel pageVm;
            bool containsVm = m_savedViewModels.TryGetValue(typeof(CreateGameUcViewModel), out pageVm);
            if (!containsVm)
            {
                var vm = new CreateGameUcViewModel();
                vm.GameCreated += OnGameChoosed;
                pageVm = vm;
            }

            CurrentPage = pageVm;

            IsMenuOpened = false;
        }
        void OpenLoadGamePage()
        {
            IPageViewModel pageVm;

            bool containsVm = m_savedViewModels.TryGetValue(typeof(LoadGameUcViewModel), out pageVm);
            if (!containsVm)
            {
                var vm = new LoadGameUcViewModel(new DialogService());
                vm.GameLoaded += OnGameChoosed;

                pageVm = vm;
            }

            CurrentPage = pageVm;

            IsMenuOpened = false;
        }
        void OnWindowClosing()
        {
            LoggingHelper.Instance.Info("The program was closed");
        }

        void OnGameChoosed(object sender, GameEventArgs e)
        {
            var vm = new TraderGameUcViewModel(e.GameId);
            CurrentPage = vm;

            LoggingHelper.Instance.Info($"The game #{e.GameId} was loaded");
        }

        #endregion
    }
}
