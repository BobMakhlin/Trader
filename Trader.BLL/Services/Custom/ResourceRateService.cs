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
    public class ResourceRateService : GenericService<ResourceRate, ResourceRateDto, int>
    {
        public ResourceRateService(IGenericRepository<ResourceRate, int> repository) : base(repository)
        {
        }
    }
}
