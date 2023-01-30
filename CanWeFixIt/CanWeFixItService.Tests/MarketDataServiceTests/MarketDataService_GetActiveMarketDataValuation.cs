using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Repositories;
using CanWeFixItService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CanWeFixItService.Tests.MarketDataServiceTests
{
    /// <summary>
    /// Test class to test the service method of active market data valuation
    /// </summary>
    public class MarketDataService_GetActiveMarketDataValuation
    {
        private IMarketDataService marketDataService;
        private Mock<IMarketDataRepository> mockMarketDataRepository;

        /// <summary>
        /// Setup tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockMarketDataRepository = new Mock<IMarketDataRepository>();

            var mockLogger = new Mock<ILogger<MarketDataService>>();

            marketDataService = new MarketDataService(mockMarketDataRepository.Object, mockLogger.Object);
        }

        /// <summary>
        /// Test to return active market data valuation
        /// </summary>
        [Test]
        public void MarketDataService_ShouldReturnActiveMarketDataValuation()
        {
            long expectedValuation = 3333;

            mockMarketDataRepository.Setup(e => e.getActiveMarketDataValuation()).Returns(expectedValuation);

            MarketValuationDto expectedMarketValuatuion = new MarketValuationDto();

            expectedMarketValuatuion=new MarketValuationDto { name = "DataValueTotal", total = 3333 };

            var result = marketDataService.getActiveMarketDataValuation();

            Assert.AreEqual(1, result.Count());

            Assert.IsTrue(result[0].Equals(expectedMarketValuatuion));
            
        }
    }
}

