using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.Logging.Helpers;
using Trader.WPF.Helpers;
using Trader.WPF.Infrastructure.MyEventArgs;
using Trader.WPF.ViewModels.PageViewModels.Common;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels.PageViewModels.Custom
{
    class LoadGameUcViewModel : BindableBase, IPageViewModel
    {
        #region Fields
        ObservableCollection<GameDto> m_games;
        GameDto m_selectedGame;
        bool m_progressBarAnimationEnabled;
        bool m_uiEnabled;

        IGenericService<Game, GameDto, int> m_gameService;

        IDialogService m_dialogService;
        #endregion

        #region Constructors
        public LoadGameUcViewModel(IDialogService dialogService)
        {
            m_dialogService = dialogService;

            InitCommands();
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
            LoggingHelper.Instance.Info($"The user wants to load the game #{e.GameId}");

            var temp = GameLoaded;
            temp?.Invoke(this, e);
        }

        void InitCommands()
        {
            OnPageLoadedCommand = new RelayCommand(OnPageLoadedAsync);
            LoadSelectedGameCommand = new RelayCommand(LoadSelectedGame);
            DeleteSelectedGameCommand = new RelayCommand(DeleteSelectedGame);
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
                m_dialogService.MessageBoxOk("Loading error", "Please select a game and try again");
            }
            catch (Exception)
            {
                m_dialogService.MessageBoxOk("Loading error", "Something has gone wrong. Please, try again");
            }
        }
        void DeleteSelectedGame()
        {
            if (SelectedGame == null)
            {
                m_dialogService.MessageBoxOk("Removing error", "Please select a game and try again");
                return;
            }

            try
            {
                m_dialogService.MessageBoxYesNo
                (
                    "Question", 
                    "Are you sure you want to delete this game?",
                    async (IDialogResult answer) =>
                    {
                        if (answer.Result == ButtonResult.Yes)
                        {
                            // Remove from the db.
                            int gameToRemoveId = SelectedGame.GameId;
                            await m_gameService.CallUpRemoveGameByIdAsync(gameToRemoveId);

                            // Update the ui.
                            Games.Remove(SelectedGame);
                            SelectedGame = Games.FirstOrDefault();

                            LoggingHelper.Instance.Debug($"The game #{gameToRemoveId} was removed");
                        }
                    }
                );
            }
            catch (Exception ex)
            {
                m_dialogService.MessageBoxOk("Removing error", "Something has gone wrong. Please, try again");

                LoggingHelper.Instance.Error("Can't remove the game", ex);
            }
        }
        #endregion
    }
}
