using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Custom;
using Trader.DAL.DbModels;
using Trader.DAL.Repositories;
using Trader.Repository.Common;
using System.Data.Entity;

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
                .RegisterType(typeof(ResourceRateService))
                .As(typeof(IGenericService<ResourceRate, ResourceRateDto, int>));
            builder
                .RegisterType(typeof(ResourceRateRepository))
                .As(typeof(IGenericRepository<ResourceRate, int>));

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
