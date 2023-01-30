using System;
using System.Collections.Generic;
using CanWeFixItRepository.Models;

namespace CanWeFixItRepository.Repositories
{
    /// <summary>
    /// Interface for the MarketDataRepository
    /// </summary>
    public interface IMarketDataRepository
    {
        /// <summary>
        /// Get active market Data entity records which has sedol 
        /// </summary>
        /// <returns>Set of MarketData entities</returns>
        IEnumerable<MarketData> getActiveMarketDataWithSedol();

        /// <summary>
        /// Get the total of the sum of all currently active MarketData 
        /// </summary>
        /// <returns>total value</returns>
        long getActiveMarketDataValuation();
    }
}

