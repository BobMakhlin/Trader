using Autofac;
using System.Data.Entity;
using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Custom;
using Trader.DAL.DbModels;
using Trader.DAL.Repositories;
using Trader.Repository.Common;

namespace Trader.BLL.Infrastructure
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType(typeof(ResourceService))
                .As(typeof(IGenericService<Resource, ResourceDto, int>));
            builder
                .RegisterType(typeof(ResourceRepository))
                .As(typeof(IGenericRepository<Resource, int>));

            builder
                .RegisterType(typeof(TradingResourceService))
                .As(typeof(IGenericService<TradingResource, TradingResourceDto, int>));
            builder
                .RegisterType(typeof(TradingResourceRepository))
                .As(typeof(IGenericRepository<TradingResource, int>));

            builder
                .RegisterType(typeof(TradingResourceRateService))
                .As(typeof(IGenericService<TradingResourceRate, TradingResourceRateDto, int>));
            builder
                .RegisterType(typeof(TradingResourceRateRepository))
                .As(typeof(IGenericRepository<TradingResourceRate, int>));

            builder
                .RegisterType(typeof(ResourceWalletService))
                .As(typeof(IGenericService<ResourceWallet, ResourceWalletDto, int>));
            builder
               .RegisterType(typeof(ResourceWalletRepository))
               .As(typeof(IGenericRepository<ResourceWallet, int>));

            builder
                .RegisterType(typeof(ResourceWalletTransactionService))
                .As(typeof(IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int>));
            builder
               .RegisterType(typeof(ResourceWalletTransactionRepository))
               .As(typeof(IGenericRepository<ResourceWalletTransaction, int>));

            builder
                .RegisterType(typeof(GameService))
                .As(typeof(IGenericService<Game, GameDto, int>));
            builder
                .RegisterType(typeof(GameRepository))
                .As(typeof(IGenericRepository<Game, int>));

            builder
                .RegisterType(typeof(TraderContext))
                .As(typeof(DbContext));
        }
    }
}
