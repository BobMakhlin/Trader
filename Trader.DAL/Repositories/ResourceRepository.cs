using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class ResourceRepository : GenericRepository<Resource, int>
    {
        public ResourceRepository(DbContext context) : base(context)
        {
        }
    }
}
