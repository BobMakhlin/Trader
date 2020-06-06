using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class ResourceWalletRepository : GenericRepository<ResourceWallet, int>
    {
        public ResourceWalletRepository(DbContext context) : base(context)
        {
        }
    }
}
