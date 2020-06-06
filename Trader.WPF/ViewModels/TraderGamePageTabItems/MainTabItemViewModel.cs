using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.Helpers.Common.ResourceRateHelpers;
using Trader.Helpers.Common.ResourceRatesHelpers;
using Trader.Logging.Helpers;
using Trader.WPF.ViewModels.PageViewModels.Custom;
using WPF.Common.Helpers;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels.TraderGamePageTabItems
{
    class MainTabItemViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        TraderGameUcViewModel m_parentViewModel;

        IGenericService<TradingResourceRate, TradingResourceRateDto, int> m_resourceRateService;
        IGenericService<ResourceWallet, ResourceWalletDto, int> m_resourceWalletService;
        IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> m_walletTransactionService;

        ResourcesRatesHelper m_rateHelper;

        List<TradingResourceRateDto> m_tradingResourcesRates;
        #endregion

        #region Constructors
        private MainTabItemViewModel()
        {
        }
        #endregion

        #region IDataErrorInfo implementation
        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                string error = "";

                switch (columnName)
                {
                    case "GoldToSpend":
                        if (GoldToSpend <= 0)
                        {
                            error = "The count of gold can't be less than 0";
                        }
                        break;
                    case "ResourceToSellAmount":
                        if (ResourceToSellAmount <= 0)
                        {
                            error = "The count can't be less than 0";
                        }
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Properties
        public ICommand BuyResourceCommand { get; set; }
        public ICommand SellResourceCommand { get; set; }

        /// <summary>
        /// Resources' rates by current move of the current game.
        /// </summary>
        public List<TradingResourceRateDto> TradingResourcesRates
        {
            get => m_tradingResourcesRates;
            set
            {
                m_tradingResourcesRates = value;
                RaisePropertyChanged();
            }
        }
        public List<TradingResourceDto> TradingResources => m_parentViewModel.TradingResources;

        public double GoldToSpend { get; set; }
        public TradingResourceDto ResourceToBuy { get; set; }

        public double ResourceToSellAmount { get; set; }
        public TradingResourceDto ResourceToSell { get; set; }
        #endregion

        #region Methods
        public static async Task<MainTabItemViewModel> CreateAsync(TraderGameUcViewModel parentViewModel)
        {
            var vm = new MainTabItemViewModel()
            {
                m_parentViewModel = parentViewModel
            };
            await vm.InitAsync();

            return vm;
        }

        async Task CreateResourcesRatesAsync()
        {
            TradingResourcesRates = await m_rateHelper.CreateResourcesRatesAsync
            (
                m_parentViewModel.CurrentGame,
                m_parentViewModel.TradingResources
            );
        }

        async Task InitAsync()
        {
            InitCommands();
            InitServices();

            m_rateHelper = new ResourcesRatesHelper(m_resourceRateService, new ResourceRateGenerator());
            m_parentViewModel.MoveFinished += OnMoveFinishedAsync;

            await LoadResourcesRatesAsync();
        }

        async void OnMoveFinishedAsync(object sender, EventArgs e)
        {
            await CreateResourcesRatesAsync();
        }

        void InitCommands()
        {
            BuyResourceCommand = new RelayCommand(BuyResourceAsync, CanBuyResource);
            SellResourceCommand = new RelayCommand(SellResourceAsync, CanSellResource);
        }
        void InitServices()
        {
            m_resourceRateService = MyContainer.Resolve<IGenericService<TradingResourceRate, TradingResourceRateDto, int>>();
            m_resourceWalletService = MyContainer.Resolve<IGenericService<ResourceWallet, ResourceWalletDto, int>>();
            m_walletTransactionService = MyContainer.Resolve<IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int>>();
        }

        /// <summary>
        /// Load resources rates from DB by current move.
        /// </summary>
        async Task LoadResourcesRatesAsync()
        {
            var predicate = PredicateBuilder.New<TradingResourceRateDto>
            (
                rate => rate.GameId == m_parentViewModel.CurrentGame.GameId
            );
            predicate.And
            (
                rate => rate.MoveNumber == m_parentViewModel.CurrentGame.CurrentMoveNumber
            );

            var resourceRates = await m_resourceRateService.WhereAsync(predicate);
            TradingResourcesRates = new List<TradingResourceRateDto>(resourceRates);
        }

        async void BuyResourceAsync()
        {
            var goldWallet = m_parentViewModel.CurrentGameWallets.Find(w => w.ResourceId == 6); // The id of the gold is 6.
            var destWallet = m_parentViewModel.CurrentGameWallets.Find(w => w.ResourceId == ResourceToBuy.ResourceId);

            var selectedResourceRate = TradingResourcesRates.Find
            (
                rate => rate.TradingResourceId == ResourceToBuy.ResourceId
            );

            double amountToSendToDestWallet = GoldToSpend / selectedResourceRate.TradingResourcePrice;

            try
            {
                await m_walletTransactionService.SendResourcesAsync(goldWallet, destWallet, GoldToSpend, amountToSendToDestWallet);

                m_parentViewModel.OnTransactionFinished(this, EventArgs.Empty);

                LoggingHelper.Instance.Info($"The user has bought [{amountToSendToDestWallet} {selectedResourceRate.TradingResourceName}] for [{GoldToSpend} Gold]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Transaction failed");

                LoggingHelper.Instance.Info($"Can't buy [{amountToSendToDestWallet} {ResourceToBuy.ResourceName}] for [{GoldToSpend} Gold]", ex);
            }
        }
        bool CanBuyResource()
        {
            return GoldToSpend > 0 && ResourceToBuy != null;
        }

        async void SellResourceAsync()
        {
            var sourceWallet = m_parentViewModel.CurrentGameWallets.Find(w => w.ResourceId == ResourceToSell.ResourceId);
            var goldWallet = m_parentViewModel.CurrentGameWallets.Find(w => w.ResourceId == 6); // The id of the gold is 6.

            var selectedResourceRate = TradingResourcesRates.Find
            (
                rate => rate.TradingResourceId == ResourceToSell.ResourceId
            );

            double amountToSendToDestWallet = selectedResourceRate.TradingResourcePrice * ResourceToSellAmount;

            try
            {
                await m_walletTransactionService.SendResourcesAsync(sourceWallet, goldWallet, ResourceToSellAmount, amountToSendToDestWallet);

                LoggingHelper.Instance.Info($"The user has sold [{ResourceToSellAmount} {ResourceToSell.ResourceName}] for [{amountToSendToDestWallet} Gold]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");

                LoggingHelper.Instance.Info($"Can't sell [{ResourceToSellAmount} {ResourceToSell.ResourceName}] for [{amountToSendToDestWallet} Gold]", ex);
            }

            m_parentViewModel.OnTransactionFinished(this, EventArgs.Empty);
        }
        bool CanSellResource()
        {
            return ResourceToSellAmount > 0 && ResourceToSell != null;
        }
        #endregion
    }
}
