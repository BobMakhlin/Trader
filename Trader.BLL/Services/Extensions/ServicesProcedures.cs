using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
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

        /// <summary>
        /// Call the function sf_GetWalletBalance.
        /// </summary>
        public static double? CallSfGetWalletBalance
        (
            this IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> service,
            int gameId,
            int resourceId
        )
        {
            DbContext context = MyContainer.Resolve<DbContext>();

            var gameIdParam = new SqlParameter("@GameId", gameId);
            var resourceIdParam = new SqlParameter("@ResourceId", resourceId);

            var funcRes = context.Database.SqlQuery<double?>("select dbo.sf_GetWalletBalance(@GameId, @ResourceId)", gameIdParam, resourceIdParam);
            double? balance = funcRes.Single();

            return balance;
        }
        /// <summary>
        /// Call the function sf_GetWalletBalance asynchronously.
        /// </summary>
        public static async Task<double?> CallSfGetWalletBalanceAsync
        (
            this IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> service,
            int gameId,
            int resourceId
        )
        {
            return await Task.Run(() => service.CallSfGetWalletBalance(gameId, resourceId));
        }
    }
}
