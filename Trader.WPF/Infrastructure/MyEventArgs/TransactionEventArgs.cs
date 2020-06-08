using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;

namespace Trader.WPF.Infrastructure.MyEventArgs
{
    class TransactionEventArgs : EventArgs
    {
        public ResourceWalletDto SourceWallet { get; set; }
        public double AmountWithdrawnFromSourceWallet { get; set; }
        public ResourceWalletDto DestWallet { get; set; }
        public double AmountSentToDestWallet { get; set; }
    }
}
