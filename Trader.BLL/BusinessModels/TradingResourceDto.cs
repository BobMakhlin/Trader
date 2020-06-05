using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.BLL.BusinessModels
{
    public class TradingResourceDto
    {
        public int ResourceId { get; set; }

        public string ResourceName { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }
    }
}
