using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.DAL.DbModels
{
    public class ResourceWalletTransaction
    {
        public int ResourceWalletTransactionId { get; set; }

        public int GameId { get; set; }
        public int ResourceId { get; set; }
        public virtual ResourceWallet ResourceWallet { get; set; }

        public double ResourceCount { get; set; }
    }
}
