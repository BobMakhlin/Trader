using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.DAL.DbModels
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        [StringLength(64)]
        public string GameName { get; set; }

        public DateTime Date { get; set; }

        public int CurrentMoveNumber { get; set; }

        public virtual ICollection<TradingResourceRate> ResourceRates { get; set; }
        public virtual ICollection<ResourceWallet> ResourceWallets { get; set; }
    }
}
