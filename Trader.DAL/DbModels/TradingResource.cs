using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trader.DAL.DbModels
{
    public class TradingResource
    {
        [Key]
        [ForeignKey("Resource")]
        public int ResourceId { get; set; }
        public virtual Resource Resource { get; set; }

        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        public virtual ICollection<TradingResourceRate> TradingResourceRates { get; set; }
    }
}
