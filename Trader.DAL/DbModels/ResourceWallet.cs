using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trader.DAL.DbModels
{
    public class ResourceWallet
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Resource")]
        public int ResourceId { get; set; }
        public virtual Resource Resource { get; set; }

        public virtual ICollection<ResourceWalletTransaction> ResourceWalletTransactions { get; set; }
    }
}
