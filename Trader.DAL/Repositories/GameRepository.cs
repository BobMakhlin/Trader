using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.DAL.Repositories
{
    public class GameRepository : GenericRepository<Game, int>
    {
        public GameRepository(DbContext context) : base(context)
        {
        }
    }
}
