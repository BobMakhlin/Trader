namespace Trader.DAL.DbModels
{
    using System.Data.Entity;
    using Trader.DAL.DbInitializers;

    public class TraderContext : DbContext
    {
        public TraderContext()
            : base("name=TraderContext")
        {
            Database.SetInitializer(new MyDbInitializer());
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<TradingResource> TradingResources { get; set; }
        public virtual DbSet<TradingResourceRate> TradingResourcesRates { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
    }
}