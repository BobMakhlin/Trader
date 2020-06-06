using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class TradingResourceRepository : GenericRepository<TradingResource, int>
    {
        public TradingResourceRepository(DbContext context) : base(context)
        {
        }
    }
}
