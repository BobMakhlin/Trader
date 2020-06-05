﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.WPF.ViewModels.PageViewModels.Common;
using Trader.WPF.ViewModels.TraderGamePageTabItems;
using WPF.Common.Helpers;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels.PageViewModels.Custom
{
    class TraderGameUcViewModel : NotifyPropertyChanged, IPageViewModel
    {
        #region Fields
        IGenericService<Game, GameDto, int> m_gameService;
        IGenericService<Resource, ResourceDto, int> m_resourcesService;
        IGenericService<TradingResource, TradingResourceDto, int> m_tradingResourcesService;
        IGenericService<ResourceWallet, ResourceWalletDto, int> m_resourceWalletService;

        int m_gameId;

        ExchangerTabItemViewModel m_exchangerTabItemVm;
        MainTabItemViewModel m_mainTabItemVm;
        BalanceTabItemViewModel m_balanceTabItemVm;
        #endregion

        #region Constructors
        public TraderGameUcViewModel(int gameId)
        {
            m_gameId = gameId;

            InitServices();
            InitCommands();
        }
        #endregion

        #region Events
        public event EventHandler MoveFinished;
        public event EventHandler TransactionFinished;
        #endregion

        #region Properties
        public ICommand OnPageLoadedCommand { get; set; }
        public ICommand FinishMoveCommand { get; set; }

        public GameDto CurrentGame { get; set; }
        public List<ResourceDto> Resources { get; set; }
        public List<TradingResourceDto> TradingResources { get; set; }
        /// <summary>
        /// Wallets by current game.
        /// </summary>
        public List<ResourceWalletDto> CurrentGameWallets { get; set; }

        public ExchangerTabItemViewModel ExchangerTabItemVm
        {
            get => m_exchangerTabItemVm;
            set
            {
                m_exchangerTabItemVm = value;
                RaisePropertyChanged();
            }
        }
        public MainTabItemViewModel MainTabItemVm
        {
            get => m_mainTabItemVm;
            set
            {
                m_mainTabItemVm = value;
                RaisePropertyChanged();
            }
        }
        public BalanceTabItemViewModel BalanceTabItemVm
        {
            get => m_balanceTabItemVm;
            set
            {
                m_balanceTabItemVm = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raise the event TransactionFinished.
        /// </summary>
        public void RaiseTransactionFinished(object sender, EventArgs e)
        {
            TransactionFinished?.Invoke(sender, e);
        }

        void InitServices()
        {
            m_gameService = MyContainer.Resolve<IGenericService<Game, GameDto, int>>();
            m_resourcesService = MyContainer.Resolve<IGenericService<Resource, ResourceDto, int>>();
            m_tradingResourcesService = MyContainer.Resolve<IGenericService<TradingResource, TradingResourceDto, int>>();
            m_resourceWalletService = MyContainer.Resolve<IGenericService<ResourceWallet, ResourceWalletDto, int>>();
        }
        void InitCommands()
        {
            OnPageLoadedCommand = new RelayCommand(OnPageLoadedAsync);
            FinishMoveCommand = new RelayCommand(FinishMoveAsync);
        }
        async Task InitInnerViewModelsAsync()
        {
            ExchangerTabItemVm = new ExchangerTabItemViewModel(this);
            MainTabItemVm = await MainTabItemViewModel.CreateAsync(this);
            BalanceTabItemVm = await BalanceTabItemViewModel.CreateAsync(this);
        }

        async void OnPageLoadedAsync()
        {
            // Get current game.
            CurrentGame = await m_gameService.GetAsync(g => g.GameId == m_gameId);

            // Get resources.
            var resource = await m_resourcesService.GetAllAsync();
            Resources = resource.ToList();

            // Get trading resources.
            var tradingResources = await m_tradingResourcesService.GetAllAsync();
            TradingResources = tradingResources.ToList();

            // Get wallets by this game.
            var wallets = await m_resourceWalletService.WhereAsync(w => w.GameId == m_gameId);
            CurrentGameWallets = wallets.ToList();

            // Init TabItems' ViewModels.
            await InitInnerViewModelsAsync();
        }
        async void FinishMoveAsync()
        {
            CurrentGame.CurrentMoveNumber++;
            CurrentGame = await m_gameService.AddOrUpdateAsync(CurrentGame);

            MoveFinished?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
