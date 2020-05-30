namespace Trader.DAL.DbModels
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Trader.DAL.DbInitializers;

    public class TraderContext : DbContext
    {
        public TraderContext()
            : base("name=TraderContext")
        {
            Database.SetInitializer(new MyDbInitializer());
        }

        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceRate> ResourceRates { get; set; }
    }
}