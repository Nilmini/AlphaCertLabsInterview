using System;
using System.Collections.Generic;
using CanWeFixItService.Models;

namespace CanWeFixItService.Services
{
    /// <summary>
    /// Interface for the MarketDataService class
    /// </summary>
    public interface IMarketDataService
    {
        /// <summary>
        /// Get active market data which has sedol
        /// </summary>
        /// <returns> A list of MarketDataDto </returns>
        List<MarketDataDto> getActiveMarketDataWithSedol();

        /// <summary>
        /// Get active market data valuation
        /// </summary>
        /// <returns> A list of MarketValuationDto </returns>
        List<MarketValuationDto> getActiveMarketDataValuation();
    }
}

