using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CanWeFixItRepository.Repositories
{
    /// <summary>
    /// Class of MarketDataRepository
    /// </summary>
    public class MarketDataRepository : IMarketDataRepository
    {
        private readonly ILogger _logger;
        private readonly CanWeFixItContext _canWeFixItContext;

        /// <summary>
        /// Constructor of the MarketDataRepository class
        /// </summary>
        /// <param name="canWeFixItContext">Instance of CanWeFixItContext</param>
        /// <param name="logger">Instance of Logger</param>
        public MarketDataRepository(CanWeFixItContext canWeFixItContext, ILogger<MarketDataRepository> logger)
        {
            _canWeFixItContext = canWeFixItContext;
            _logger = logger;
        }

        /// <summary>
        /// Get active market Data entity records which has sedol 
        /// </summary>
        /// <returns>Set of MarketData entities</returns>
        public IEnumerable<MarketData> getActiveMarketDataWithSedol()
        {
            _logger.LogInformation("MarketDataRepository.getActiveMarketDataWithSedol() called.");

            return _canWeFixItContext.MarketData
                .Include(e => e.Sedol)
                .ThenInclude(e => e.Instrument)
                .Where(e => e.Active == true && e.Sedol != null && e.Sedol.Instrument != null);
        }

        /// <summary>
        /// Get the total of the sum of all currently active MarketData 
        /// </summary>
        /// <returns>total value</returns>
        public long getActiveMarketDataValuation()
        {
            _logger.LogInformation("MarketDataRepository.getActiveMarketDataValuation() called.");

            return _canWeFixItContext.MarketData
                .Where(e => e.Active).Sum(e => e.DataValue).Value;
        }
    }
}

