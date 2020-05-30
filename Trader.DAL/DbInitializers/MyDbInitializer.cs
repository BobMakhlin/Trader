using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.DAL.DbModels;

namespace Trader.DAL.DbInitializers
{
    class MyDbInitializer : DropCreateDatabaseIfModelChanges<TraderContext>
    {
        protected override void Seed(TraderContext context)
        {
            var wood = context.Resources.Add(new Resource { ResourceName = "Wood" });
            var coal = context.Resources.Add(new Resource { ResourceName = "Coal" });
            var iron = context.Resources.Add(new Resource { ResourceName = "Iron" });
            var silver = context.Resources.Add(new Resource { ResourceName = "Silver" });
            var gold = context.Resources.Add(new Resource { ResourceName = "Gold" });
            var diamond = context.Resources.Add(new Resource { ResourceName = "Diamond" });

            context.SaveChanges();

            // Create procedure up_RemoveGameById.

            string createProc =
            @"
                create proc up_RemoveGameById
	                @GameId int 
                as
	                delete rr
	                from dbo.ResourceRates rr
	                where rr.GameId = @GameId

	                delete g
	                from dbo.Games g
	                where g.GameId = @GameId
            ";
            context.Database.ExecuteSqlCommand(createProc);
        }
    }
}
