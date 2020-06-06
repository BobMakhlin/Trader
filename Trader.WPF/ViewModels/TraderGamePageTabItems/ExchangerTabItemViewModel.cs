using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.Logging.Helpers;
using Trader.WPF.ViewModels.PageViewModels.Custom;
using WPF.Common.Helpers;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels.TraderGamePageTabItems
{
    class ExchangerTabItemViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        IGenericService<TradingResourceRate, TradingResourceRateDto, int> m_resourceRateService;
        IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> m_walletTransactionService;

        TraderGameUcViewModel m_parentViewModel;
        #endregion

        #region Constructors
        public ExchangerTabItemViewModel(TraderGameUcViewModel parentViewModel)
        {
            m_parentViewModel = parentViewModel;

            InitCommands();
            InitServices();
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
                    case "SourceResourceCount":
                        if (SourceResourceCount <= 0)
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
        public ICommand ConvertResourceCommand { get; set; }

        public List<TradingResourceRateDto> TradingResourcesRates { get; set; }
        public double SourceResourceCount { get; set; }
        public List<TradingResourceDto> TradingResources => m_parentViewModel.TradingResources;
        public TradingResourceDto SourceResource { get; set; }
        public TradingResourceDto DestResource { get; set; }
        #endregion

        #region Methods
        void InitCommands()
        {
            ConvertResourceCommand = new RelayCommand(ConvertResource, CanConvertResource);
        }
        void InitServices()
        {
            m_resourceRateService = MyContainer.Resolve<IGenericService<TradingResourceRate, TradingResourceRateDto, int>>();
            m_walletTransactionService = MyContainer.Resolve<IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int>>();
        }
        async void ConvertResource()
        {
            // Get the price of the source resource.

            var sourceResourcePredicate = PredicateBuilder.New<TradingResourceRateDto>(rate => rate.GameId == m_parentViewModel.CurrentGame.GameId);
            sourceResourcePredicate.And(rate => rate.MoveNumber == m_parentViewModel.CurrentGame.CurrentMoveNumber);
            sourceResourcePredicate.And(rate => rate.TradingResourceId == SourceResource.ResourceId);
            var sourceResourceRate = await m_resourceRateService.GetAsync(sourceResourcePredicate);

            // Get the price of the destination resource.

            var destResourcePredicate = PredicateBuilder.New<TradingResourceRateDto>(rate => rate.GameId == m_parentViewModel.CurrentGame.GameId);
            destResourcePredicate.And(rate => rate.MoveNumber == m_parentViewModel.CurrentGame.CurrentMoveNumber);
            destResourcePredicate.And(rate => rate.TradingResourceId == DestResource.ResourceId);
            var destResourceRate = await m_resourceRateService.GetAsync(destResourcePredicate);

            // Get source and destination wallets.

            var sourceWallet = m_parentViewModel.CurrentGameWallets.Find(w => w.ResourceId == SourceResource.ResourceId);
            var destWallet = m_parentViewModel.CurrentGameWallets.Find(w => w.ResourceId == DestResource.ResourceId);

            // Send resources to the destination wallet.

            double amountToSendToDestWallet
                = SourceResourceCount * sourceResourceRate.TradingResourcePrice / destResourceRate.TradingResourcePrice;

            try
            {
                await m_walletTransactionService.SendResourcesAsync(sourceWallet, destWallet, SourceResourceCount, amountToSendToDestWallet);

                LoggingHelper.Instance.Info($"The user has exchanged [{SourceResourceCount} {SourceResource.ResourceName}] for [{amountToSendToDestWallet} {DestResource.ResourceName}]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Transaction failed");

                LoggingHelper.Instance.Info($"Can't exchange [{SourceResourceCount} {SourceResource.ResourceName}] for [{amountToSendToDestWallet} {DestResource.ResourceName}]", ex);
            }

            m_parentViewModel.OnTransactionFinished(this, EventArgs.Empty);
        }
        bool CanConvertResource()
        {
            return
            (
                SourceResource != DestResource
                && SourceResource != null
                && DestResource != null
                && SourceResourceCount > 0
            );
        }
        #endregion
    }
}

