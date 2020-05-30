using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class ResourceRateRepository : GenericRepository<ResourceRate, int>
    {
        public ResourceRateRepository(DbContext context) : base(context)
        {
        }
    }
}
