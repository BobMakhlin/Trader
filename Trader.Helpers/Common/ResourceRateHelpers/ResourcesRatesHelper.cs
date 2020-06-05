using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.Helpers.Common.ResourceRateHelpers;

namespace Trader.Helpers.Common.ResourceRatesHelpers
{
    public class ResourcesRatesHelper
    {
        #region Private Definitions
        IGenericService<TradingResourceRate, TradingResourceRateDto, int> m_resourceService;
        IResourceRateGenerator m_rateGenerator;
        #endregion

        public ResourcesRatesHelper(IGenericService<TradingResourceRate, TradingResourceRateDto, int> resourceService, IResourceRateGenerator rateGenerator)
        {
            m_resourceService = resourceService;
            m_rateGenerator = rateGenerator;
        }

        /// <summary>
        /// Create rate for each resource of the given collection in DB.
        /// </summary>
        /// <returns>
        /// The list of the inserted rates.
        /// </returns>
        public List<TradingResourceRateDto> CreateResourcesRates(GameDto game, List<TradingResourceDto> resources)
        {
            m_rateGenerator.GameId = game.GameId;
            m_rateGenerator.MoveNumber = game.CurrentMoveNumber;

            var rates = new List<TradingResourceRateDto>();

            foreach (var resource in resources)
            {
                var rate = m_rateGenerator.GenerateRate(resource);
                var insertedRate = m_resourceService.AddOrUpdate(rate);

                rates.Add(insertedRate);
            }

            return rates;
        }

        /// <summary>
        /// Create rate for each resource of the given collection in DB asynchronously.
        /// </summary>
        /// /// <returns>
        /// The list of the inserted rates, encapsulated in task.
        /// </returns>
        public async Task<List<TradingResourceRateDto>> CreateResourcesRatesAsync(GameDto game, List<TradingResourceDto> resources)
        {
            return await Task.Run(() => CreateResourcesRates(game, resources));
        }
    }
}
