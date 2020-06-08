using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.WPF.ViewModels.PageViewModels.Custom;

namespace Trader.WPF.ViewModels.TraderGamePageTabItems
{
    class BalanceTabItemViewModel : BindableBase
    {
        #region Fields
        List<IGrouping<string, double>> m_walletTransactions;

        TraderGameUcViewModel m_parentViewModel;

        IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> m_walletTransactionService;
        #endregion

        #region Constructors
        private BalanceTabItemViewModel()
        {
        }
        #endregion

        #region Properties
        public List<IGrouping<string, double>> WalletTransactions
        {
            get => m_walletTransactions;
            set
            {
                SetProperty(ref m_walletTransactions, value);
            }
        }
        #endregion

        #region Methods
        public async static Task<BalanceTabItemViewModel> CreateAsync(TraderGameUcViewModel parentViewModel)
        {
            var vm = new BalanceTabItemViewModel()
            {
                m_parentViewModel = parentViewModel
            };
            await vm.InitAsync();
            return vm;
        }
        public async Task LoadWalletsTransactionsAsync()
        {
            var transactionsList = await m_walletTransactionService.WhereAsync
            (
                t => t.GameId == m_parentViewModel.CurrentGame.GameId
            );
            List<IGrouping<string, double>> transactions =
                await Task.Run
                (
                    (
                       from res in m_parentViewModel.Resources
                       join tran in transactionsList
                       on res.ResourceId equals tran.ResourceId

                       into ts

                       from tran in ts.DefaultIfEmpty()

                           // If there is no transactions by current resource, pass zero.
                       group (tran != null ? tran.ResourceCount : 0) by res.ResourceName
                    )
                    .ToList
                );

            WalletTransactions = transactions;
        }

        async Task InitAsync()
        {
            InitServices();

            m_parentViewModel.TransactionFinished += OnTransactionFinishedAsync;

            await LoadWalletsTransactionsAsync();
        }
        async void OnTransactionFinishedAsync(object sender, EventArgs e)
        {
            await LoadWalletsTransactionsAsync();
        }

        void InitServices()
        {
            m_walletTransactionService = MyContainer.Resolve<IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int>>();
        }
        #endregion
    }
}
