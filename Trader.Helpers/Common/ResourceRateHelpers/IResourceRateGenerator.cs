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
