using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class TradingResourceRateRepository : GenericRepository<TradingResourceRate, int>
    {
        public TradingResourceRateRepository(DbContext context) : base(context)
        {
        }
    }
}
