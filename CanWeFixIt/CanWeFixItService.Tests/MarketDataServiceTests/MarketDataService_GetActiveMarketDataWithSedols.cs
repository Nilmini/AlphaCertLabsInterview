using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Models;
using CanWeFixItRepository.Repositories;
using CanWeFixItService.Models;
using CanWeFixItService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CanWeFixItService.Tests.MarketDataServiceTests
{
    /// <summary>
    /// Test class to test service method of the active market data with sedol
    /// </summary>
    public class MarketDataService_GetActiveMarketDataWithSedol
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
        /// Test to return active market data with sedol
        /// </summary>
        [Test]
        public void MarketDataService_ShouldReturnActiveMarketDataWithSedol()
        {
            List<MarketData> marketDataEntities = new List<MarketData>();

            marketDataEntities.Add(new MarketData { Id = 1, DataValue = 1111, Sedol = new Sedol { Instrument = new Instrument { Id = 1 } }, Active = true });
            marketDataEntities.Add(new MarketData { Id = 2, DataValue = 2222, Sedol = new Sedol { Instrument = new Instrument { Id = 2 } }, Active = true });

            mockMarketDataRepository.Setup(e => e.getActiveMarketDataWithSedol()).Returns(marketDataEntities);

            List<MarketDataDto> expectedMarketData = new List<MarketDataDto>();

            expectedMarketData.Add(new MarketDataDto { Id = 1, DataValue = 1111, InstrumentId = 1, Active = true });
            expectedMarketData.Add(new MarketDataDto { Id = 2, DataValue = 2222, InstrumentId = 2, Active = true });

            var result = marketDataService.getActiveMarketDataWithSedol();

            Assert.AreEqual(expectedMarketData.Count(), result.Count());

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.IsTrue(result[i].Equals(expectedMarketData[i]));
            }
        }
    }
}

