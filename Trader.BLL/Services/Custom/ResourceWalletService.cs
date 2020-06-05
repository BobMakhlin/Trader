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
    public class ResourceWalletService : GenericService<ResourceWallet, ResourceWalletDto, int>
    {
        public ResourceWalletService(IGenericRepository<ResourceWallet, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<ResourceWallet, ResourceWalletDto>()
                        .ForMember(w => w.ResourceName, opt => opt.MapFrom(w => w.Resource.ResourceName));

                    ce.CreateMap<ResourceWalletDto, ResourceWallet>();

                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
