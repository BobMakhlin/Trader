using System.Data.Entity;
using Trader.DAL.DbModels;
using Trader.Logging.Helpers;

namespace Trader.DAL.DbInitializers
{
    class MyDbInitializer : DropCreateDatabaseIfModelChanges<TraderContext>
    {
        protected override void Seed(TraderContext context)
        {
            // Create resources.

            var wood = context.Resources.Add(new Resource
            {
                ResourceName = "Wood"
            });
            var coal = context.Resources.Add(new Resource
            {
                ResourceName = "Coal"
            });
            var iron = context.Resources.Add(new Resource
            {
                ResourceName = "Iron"
            });
            var silver = context.Resources.Add(new Resource
            {
                ResourceName = "Silver"
            });
            var diamond = context.Resources.Add(new Resource
            {
                ResourceName = "Diamond"
            });
            var gold = context.Resources.Add(new Resource
            {
                ResourceName = "Gold"
            });

            context.SaveChanges();

            // Create trading resources.
            // Insert all resources except the gold.

            context.TradingResources.Add(new TradingResource
            {
                ResourceId = wood.ResourceId,
                MinPrice = 0.01,
                MaxPrice = 0.25
            });
            context.TradingResources.Add(new TradingResource
            {
                ResourceId = coal.ResourceId,
                MinPrice = 0.1,
                MaxPrice = 0.375
            });
            context.TradingResources.Add(new TradingResource
            {
                ResourceId = iron.ResourceId,
                MinPrice = 0.1,
                MaxPrice = 0.5
            });
            context.TradingResources.Add(new TradingResource
            {
                ResourceId = silver.ResourceId,
                MinPrice = 0.2,
                MaxPrice = 0.75
            });
            context.TradingResources.Add(new TradingResource
            {
                ResourceId = diamond.ResourceId,
                MinPrice = 1,
                MaxPrice = 12
            });

            context.SaveChanges();

            // Create procedure up_RemoveGameById.

            string createProc =
            @"
                create proc up_RemoveGameById
	                @GameId int 
                as
	                delete rr
	                from dbo.TradingResourceRates rr
	                where rr.GameId = @GameId

					delete wt
					from dbo.ResourceWalletTransactions wt
					where wt.GameId = @GameId

					delete w
					from dbo.ResourceWallets w
					where w.GameId = @GameId

	                delete g
	                from dbo.Games g
	                where g.GameId = @GameId
            ";
            context.Database.ExecuteSqlCommand(createProc);

            // Create function sf_GetWalletBalance.

            string createFunc =
            @"
                create function sf_GetWalletBalance
                (
	                @GameId int,
	                @ResourceId int
                )
                returns float

                begin
	                return 
	                (
		                select sum(t.ResourceCount)
		                from dbo.ResourceWalletTransactions t
		                where t.GameId = @GameId and t.ResourceId = @ResourceId
	                )
                end
            ";
            context.Database.ExecuteSqlCommand(createFunc);

            LoggingHelper.Instance.Info("DB creatiation: succeeded");
        }
    }
}
