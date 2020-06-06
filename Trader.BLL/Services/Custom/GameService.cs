using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.BLL.Services.Custom
{
    public class GameService : GenericService<Game, GameDto, int>
    {
        public GameService(IGenericRepository<Game, int> repository) : base(repository)
        {
        }
    }
}
