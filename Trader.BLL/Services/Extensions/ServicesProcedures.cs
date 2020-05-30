using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;

namespace Trader.BLL.Services.Extensions
{
    public static class ServicesProcedures
    {
        /// <summary>
        /// Calls the procedure up_RemoveGameById.
        /// </summary>
        public static async Task CallUpRemoveGameByIdAsync(this IGenericService<Game, GameDto, int> service, int gameId)
        {
            DbContext context = MyContainer.Resolve<DbContext>();

            var gameIdParam = new SqlParameter("@GameId", gameId);
            await context.Database.ExecuteSqlCommandAsync("exec up_RemoveGameById @GameId", gameIdParam);
        }
    }
}
