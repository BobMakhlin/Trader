using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.BLL.Services.Custom
{
    public class ResourceWalletTransactionService : GenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int>
    {
        public ResourceWalletTransactionService(IGenericRepository<ResourceWalletTransaction, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<ResourceWalletTransaction, ResourceWalletTransactionDto>()
                        .ForMember(t => t.ResourceName, opt => opt.MapFrom(t => t.ResourceWallet.Resource.ResourceName));

                    ce.CreateMap<ResourceWalletTransactionDto, ResourceWalletTransaction>();
                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
