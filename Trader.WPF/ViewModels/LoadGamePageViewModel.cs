using GalaSoft.MvvmLight.Command;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.WPF.Models;
using WPF.Common.Helpers;
using WPF.Common.Infrastructure;
using WPF.Common.Services;

namespace Trader.WPF.ViewModels
{
    class LoadGamePageViewModel : NotifyPropertyChanged
    {
        #region Private Definitions
        IEventAggregator m_eventAggregator;

        ObservableCollection<GameDto> games;
        GameDto m_selectedGame;
        bool m_progressBarAnimationEnabled;
        bool m_uiEnabled;

        IGenericService<Game, GameDto, int> m_gameService;

        IDialogService m_dialog;
        #endregion

        public LoadGamePageViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            InitCommands();

            m_dialog = dialogService;
            m_eventAggregator = eventAggregator;
        }

        public ObservableCollection<GameDto> Games
        {
            get => games;
            set
            {
                games = value;
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

        public ICommand OnWindowLoadedCommand { get; set; }
        public ICommand LoadSelectedGameCommand { get; set; }
        public ICommand DeleteSelectedGameCommand { get; set; }

        void InitCommands()
        {
            OnWindowLoadedCommand = new RelayCommand(OnWindowLoadedAsync);
            LoadSelectedGameCommand = new RelayCommand(LoadSelectedGame);
            DeleteSelectedGameCommand = new RelayCommand(DeleteSelectedGameAsync);
        }
        void InitServices()
        {
            m_gameService = MyContainer.Resolve<IGenericService<Game, GameDto, int>>();
        }

        async void OnWindowLoadedAsync()
        {
            InitServices();

            IsUiEnabled = false;
            IsProgressBarAnimationEnabled = true;

            var games = await Task.Run(m_gameService.GetAll);
            Games = new ObservableCollection<GameDto>(games);

            IsUiEnabled = true;
            IsProgressBarAnimationEnabled = false;
        }

        void LoadSelectedGame()
        {
            try
            {
                m_eventAggregator.GetEvent<GameIdEvent>().Publish(SelectedGame.GameId);
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
            try
            {
                var dialogResult = m_dialog.MessageBoxYesNo("Are sure you want to remove it?", "Question");

                if (dialogResult == DialogResult.Yes)
                {
                    // Remove from the db.
                    await m_gameService.CallUpRemoveGameByIdAsync(SelectedGame.GameId);

                    // Update the ui.
                    Games.Remove(SelectedGame);
                    SelectedGame = Games.FirstOrDefault();
                }
            }
            catch (NullReferenceException)
            {
                m_dialog.MessageBoxOk("Please select a game and try again", "Removing error");
            }
            catch (Exception)
            {
                m_dialog.MessageBoxOk("Something has gone wrong. Please, try again", "Removing error");
            }
        }
    }
}
