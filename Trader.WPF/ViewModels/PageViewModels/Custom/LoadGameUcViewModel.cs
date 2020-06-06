using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.Logging.Helpers;
using Trader.WPF.Infrastructure.MyEventArgs;
using Trader.WPF.ViewModels.PageViewModels.Common;
using WPF.Common.Helpers;
using WPF.Common.Helpers.MyRelayCommand;
using WPF.Common.Services;

namespace Trader.WPF.ViewModels.PageViewModels.Custom
{
    class LoadGameUcViewModel : NotifyPropertyChanged, IPageViewModel
    {
        #region Fields
        ObservableCollection<GameDto> m_games;
        GameDto m_selectedGame;
        bool m_progressBarAnimationEnabled;
        bool m_uiEnabled;

        IGenericService<Game, GameDto, int> m_gameService;

        IDialogService m_dialog;
        #endregion

        #region Constructors
        public LoadGameUcViewModel(IDialogService dialogService)
        {
            InitCommands();

            m_dialog = dialogService;
        }
        #endregion

        #region Event
        public event EventHandler<GameEventArgs> GameLoaded;
        #endregion

        #region Properties
        public ICommand OnPageLoadedCommand { get; set; }
        public ICommand LoadSelectedGameCommand { get; set; }
        public ICommand DeleteSelectedGameCommand { get; set; }

        public ObservableCollection<GameDto> Games
        {
            get => m_games;
            set
            {
                m_games = value;
                RaisePropertyChanged();
            }
        }
        public GameDto SelectedGame
        {
            get => m_selectedGame;
            set
            {
                m_selectedGame = value;
                RaisePropertyChanged();
            }
        }

        public bool IsProgressBarAnimationEnabled
        {
            get => m_progressBarAnimationEnabled;
            set
            {
                m_progressBarAnimationEnabled = value;
                RaisePropertyChanged();
            }
        }
        public bool IsUiEnabled
        {
            get => m_uiEnabled;
            set
            {
                m_uiEnabled = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Methods
        protected virtual void OnGameLoaded(GameEventArgs e)
        {
            var temp = GameLoaded;
            temp?.Invoke(this, e);
        }

        void InitCommands()
        {
            OnPageLoadedCommand = new RelayCommand(OnPageLoadedAsync);
            LoadSelectedGameCommand = new RelayCommand(LoadSelectedGame);
            DeleteSelectedGameCommand = new RelayCommand(DeleteSelectedGameAsync);
        }
        void InitServices()
        {
            m_gameService = MyContainer.Resolve<IGenericService<Game, GameDto, int>>();
        }

        async void OnPageLoadedAsync()
        {
            InitServices();

            IsUiEnabled = false;
            IsProgressBarAnimationEnabled = true;

            var games = await m_gameService.GetAllAsync();
            Games = new ObservableCollection<GameDto>(games);

            IsUiEnabled = true;
            IsProgressBarAnimationEnabled = false;
        }

        void LoadSelectedGame()
        {
            try
            {
                OnGameLoaded(new GameEventArgs(SelectedGame.GameId));
            }
            catch (NullReferenceException)
            {
                m_dialog.MessageBoxOk("Please select a game and try again", "Loading error");
            }
            catch (Exception)
            {
                m_dialog.MessageBoxOk("Something has gone wrong. Please, try again", "Loading error");
            }
        }
        async void DeleteSelectedGameAsync()
        {
            if (SelectedGame == null)
            {
                m_dialog.MessageBoxOk("Please select a game and try again", "Removing error");
                return;
            }

            try
            {
                var dialogResult = m_dialog.MessageBoxYesNo("Are sure you want to remove it?", "Question");

                if (dialogResult == DialogResult.Yes)
                {
                    // Remove from the db.
                    int gameToRemoveId = SelectedGame.GameId;
                    await m_gameService.CallUpRemoveGameByIdAsync(gameToRemoveId);

                    // Update the ui.
                    Games.Remove(SelectedGame);
                    SelectedGame = Games.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                m_dialog.MessageBoxOk("Something has gone wrong. Please, try again", "Removing error");
            }
        }
        #endregion
    }
}
