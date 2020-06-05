using System;
using Trader.BLL.BusinessModels;

namespace Trader.Helpers.Common.ResourceRateHelpers
{
    public class ResourceRateGenerator : IResourceRateGenerator
    {
        #region Private Definitions
        static Random m_random = new Random();
        #endregion

        public int MoveNumber { get; set; }
        public int GameId { get; set; }

        public TradingResourceRateDto GenerateRate(TradingResourceDto resource)
        {
            double resourcePrice = m_random.NextDoubleInRange(resource.MinPrice, resource.MaxPrice);

            return new TradingResourceRateDto
            {
                GameId = this.GameId,
                MoveNumber = this.MoveNumber,
                TradingResourceId = resource.ResourceId,
                TradingResourcePrice = resourcePrice
            };
        }
    }
}
