using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trader.DAL.DbModels
{
    public class Resource
    {
        public int ResourceId { get; set; }

        [Required]
        [StringLength(64)]
        public string ResourceName { get; set; }

        public virtual TradingResource TradingResource { get; set; }

        public virtual ICollection<ResourceWallet> ResourceWalletTransactions { get; set; }
    }
}
