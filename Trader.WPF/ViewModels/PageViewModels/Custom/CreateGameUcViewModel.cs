using Prism.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Extensions;
using Trader.DAL.DbModels;
using Trader.Helpers.Common.ResourceRateHelpers;
using Trader.Helpers.Common.ResourceRatesHelpers;
using Trader.Logging.Helpers;
using Trader.WPF.Infrastructure.MyEventArgs;
using Trader.WPF.ViewModels.PageViewModels.Common;
using WPF.Common.Helpers.MyRelayCommand;

namespace Trader.WPF.ViewModels.PageViewModels.Custom
{
    class CreateGameUcViewModel : IPageViewModel
    {
        #region Fields
        ResourcesRatesHelper m_ratesHelper;

        IGenericService<Game, GameDto, int> m_gamesService;
        IGenericService<Resource, ResourceDto, int> m_resourceService;
        IGenericService<TradingResource, TradingResourceDto, int> m_tradingResourceService;
        IGenericService<TradingResourceRate, TradingResourceRateDto, int> m_resourceRateService;
        IGenericService<ResourceWallet, ResourceWalletDto, int> m_walletService;
        IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> m_walletTransactionService;
        #endregion

        #region Constructors
        public CreateGameUcViewModel()
        {
            InitCommands();
            InitServices();

            m_ratesHelper = new ResourcesRatesHelper(m_resourceRateService, new ResourceRateGenerator());
        }

        #endregion

        #region Events
        public event EventHandler<GameEventArgs> GameCreated;
        #endregion

        #region Properties
        public RelayCommand CreateGameCommand { get; set; }

        public string GameName { get; set; }
        #endregion

        #region Methods
        protected virtual void OnGameCreated(GameEventArgs e)
        {
            LoggingHelper.Instance.Debug($"The game #{e.GameId} was created successfully");

            var temp = GameCreated;
            temp?.Invoke(this, e);
        }

        void InitCommands()
        {
            CreateGameCommand = new RelayCommand(CreateGameAsync);
        }
        void InitServices()
        {
            m_gamesService = MyContainer.Resolve<IGenericService<Game, GameDto, int>>();
            m_resourceService = MyContainer.Resolve<IGenericService<Resource, ResourceDto, int>>();
            m_tradingResourceService = MyContainer.Resolve<IGenericService<TradingResource, TradingResourceDto, int>>();
            m_resourceRateService = MyContainer.Resolve<IGenericService<TradingResourceRate, TradingResourceRateDto, int>>();
            m_walletService = MyContainer.Resolve<IGenericService<ResourceWallet, ResourceWalletDto, int>>();
            m_walletTransactionService = MyContainer.Resolve<IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int>>();
        }

        async void CreateGameAsync()
        {
            // Create the game and insert it into the DB.
            var game = new GameDto
            {
                GameName = this.GameName,
                CurrentMoveNumber = 1,
                Date = DateTime.Now
            };
            GameDto insertedGame = await m_gamesService.AddOrUpdateAsync(game);

            // Generate resources rates for this game.
            await CreateResourcesRatesAsync(insertedGame);

            // Create wallets for this game.
            await CreateWalletsAsync(insertedGame);

            // Add gold to the gold wallet.
            int goldId = 6; // The id of the gold is 6.
            var goldWallet = await m_walletService.GetAsync(w => w.GameId == insertedGame.GameId && w.ResourceId == goldId);
            await AddResourcesToWalletAsync(goldWallet, 10);

            OnGameCreated(new GameEventArgs(insertedGame.GameId));
        }

        async Task CreateResourcesRatesAsync(GameDto game)
        {
            var tradingResources = await m_tradingResourceService.GetAllAsync();
            await m_ratesHelper.CreateResourcesRatesAsync(game, tradingResources.ToList());
        }

        void CreateWallets(GameDto game)
        {
            var resources = m_resourceService.GetAll();

            foreach (var resource in resources)
            {
                var wallet = new ResourceWalletDto
                {
                    GameId = game.GameId,
                    ResourceId = resource.ResourceId
                };
                m_walletService.AddOrUpdate(wallet);
            }
        }
        async Task CreateWalletsAsync(GameDto game)
        {
            await Task.Run(() => CreateWallets(game));
        }

        async Task AddResourcesToWalletAsync(ResourceWalletDto wallet, double count)
        {
            var tran = new ResourceWalletTransactionDto
            {
                GameId = wallet.GameId,
                ResourceId = wallet.ResourceId,
                ResourceCount = count
            };
            await m_walletTransactionService.AddOrUpdateAsync(tran);
        }
        #endregion
    }
}
