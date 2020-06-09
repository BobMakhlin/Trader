using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.Models
{
    class WalletModel
    {
        public string ResourceName { get; set; }
        public List<double> Transactions { get; set; }
        public double Sum { get; set; }
    }
}
