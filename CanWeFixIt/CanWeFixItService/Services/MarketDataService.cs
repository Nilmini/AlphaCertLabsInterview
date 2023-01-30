using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Repositories;
using CanWeFixItService.Models;
using Microsoft.Extensions.Logging;

namespace CanWeFixItService.Services
{
    /// <summary>
    /// Class of MarketDataService
    /// </summary>
    public class MarketDataService : IMarketDataService
    {
        private readonly IMarketDataRepository _marketDataRepository;
        private readonly ILogger _logger;

        // If more valuation types are there, we can move this to an enum
        private readonly string marketValuationName = "DataValueTotal";

        /// <summary>
        /// Constructor of the class MarketDataService
        /// </summary>
        /// <param name="marketDataRepository">< Instance of IMarketDataRepository </param>
        /// <param name="logger">< Instance of Logger </param>
        public MarketDataService(IMarketDataRepository marketDataRepository, ILogger<MarketDataService> logger)
        {
            _marketDataRepository = marketDataRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get active market data which has sedol
        /// </summary>
        /// <returns> A list of MarketDataDto </returns>
        public List<MarketDataDto> getActiveMarketDataWithSedol()
        {
            _logger.LogInformation("MarketDataService.getActiveMarketDataWithSedol() called.");

            return _marketDataRepository.getActiveMarketDataWithSedol()
                                        .Select(e => new MarketDataDto() {
                                            Id = e.Id,
                                            DataValue = e.DataValue,
                                            InstrumentId = e.Sedol?.Instrument?.Id,
                                            Active = e.Active })
                                        .ToList();
        }

        /// <summary>
        /// Get active market data valuation
        /// </summary>
        /// <returns> A list of MarketValuationDto </returns>
        public List<MarketValuationDto> getActiveMarketDataValuation()
        {
            _logger.LogInformation("MarketDataService.getActiveMarketDataValuation() called.");

            long totalValue= _marketDataRepository.getActiveMarketDataValuation();

            _logger.LogDebug($"Data Valuation is {totalValue}");

            List<MarketValuationDto> marketValuationDtoList = new List<MarketValuationDto>();

            marketValuationDtoList.Add(new MarketValuationDto { name = marketValuationName, total = totalValue });

            return marketValuationDtoList;
        }
    }
}

