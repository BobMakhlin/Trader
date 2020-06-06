using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class ResourceWalletTransactionRepository : GenericRepository<ResourceWalletTransaction, int>
    {
        public ResourceWalletTransactionRepository(DbContext context) : base(context)
        {
        }
    }
}
