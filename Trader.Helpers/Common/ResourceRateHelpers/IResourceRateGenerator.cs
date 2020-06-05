using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;

namespace Trader.Helpers.Common.ResourceRateHelpers
{
    public interface IResourceRateGenerator
    {
        int MoveNumber { get; set; }
        int GameId { get; set; }
        TradingResourceRateDto GenerateRate(TradingResourceDto resource);
    }
}
