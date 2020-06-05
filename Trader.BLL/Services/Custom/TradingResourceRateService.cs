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
    public class TradingResourceRateService : GenericService<TradingResourceRate, TradingResourceRateDto, int>
    {
        public TradingResourceRateService(IGenericRepository<TradingResourceRate, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<TradingResourceRate, TradingResourceRateDto>()
                        .ForMember(rr => rr.TradingResourceName, opt => opt.MapFrom(rr => rr.TradingResource.Resource.ResourceName))
                        .ForMember(rr => rr.TradingResourceId, opt => opt.MapFrom(rr => rr.TradingResourceResourceId));

                    ce.CreateMap<TradingResourceRateDto, TradingResourceRate>()
                        .ForMember(rr => rr.TradingResourceResourceId, opt => opt.MapFrom(rr => rr.TradingResourceId));

                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
