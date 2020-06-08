using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using Trader.Logging.Helpers;
using Trader.WPF.Infrastructure.MyEventArgs;
using Trader.WPF.ViewModels.PageViewModels.Common;
using Trader.WPF.ViewModels.PageViewModels.Custom;
using WPF.Common.Helpers.MyRelayCommand;

/*
    «Нет ничего невозможного. Само слово говорит: „Я возможно!“ (Impossible — I'm possible)» — Одри Хепбёрн. 
*/

namespace Trader.WPF.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        #region Fields
        bool m_menuOpened;
        IPageViewModel m_currentPage;
        Dictionary<Type, IPageViewModel> m_savedViewModels;

        IDialogService m_dialogService;
        #endregion

        #region Constructors
        public MainWindowViewModel(IDialogService dialogService)
        {
            InitCommands();

            m_savedViewModels = new Dictionary<Type, IPageViewModel>();
            m_dialogService = dialogService;

            OpenEnterGameNamePage();
        }
        #endregion

        #region Properties
        public RelayCommand OpenEnterGameNamePageCommand { get; set; }
        public RelayCommand OpenLoadGamePageCommand { get; set; }
        public RelayCommand OnWindowClosingCommand { get; set; }

        public bool IsMenuOpened
        {
            get => m_menuOpened;
            set => SetProperty(ref m_menuOpened, value);
        }

        public IPageViewModel CurrentPage
        {
            get => m_currentPage;
            set => SetProperty(ref m_currentPage, value);
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
                var vm = new LoadGameUcViewModel(m_dialogService);
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
            var vm = new TraderGameUcViewModel(e.GameId, m_dialogService);
            CurrentPage = vm;

            LoggingHelper.Instance.Info($"The game #{e.GameId} was loaded");
        }

        #endregion
    }
}
