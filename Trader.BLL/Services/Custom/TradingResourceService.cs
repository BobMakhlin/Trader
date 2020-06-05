using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.BLL.Services.Custom
{
    public class TradingResourceService : GenericService<TradingResource, TradingResourceDto, int>
    {
        public TradingResourceService(IGenericRepository<TradingResource, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
               ce =>
               {
                   ce.CreateMap<TradingResource, TradingResourceDto>()
                        .ForMember(tr => tr.ResourceName, opt => opt.MapFrom(tr => tr.Resource.ResourceName));

                   ce.CreateMap<TradingResourceDto, TradingResource>();

                   ce.AddExpressionMapping();
               }
            );
            return new Mapper(mapperConfig);
        }
    }
}
