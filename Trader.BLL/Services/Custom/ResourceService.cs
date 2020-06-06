using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.BLL.Services.Custom
{
    public class ResourceService : GenericService<Resource, ResourceDto, int>
    {
        public ResourceService(IGenericRepository<Resource, int> repository) : base(repository)
        {
        }
    }
}
