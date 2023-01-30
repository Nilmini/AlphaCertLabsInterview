using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Models;
using CanWeFixItRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CanWeFixItService.Tests.RepositoryTests.MarketDataRepositoryTests
{
    /// <summary>
    /// Test class to test the repository method of active market data valuation
    /// </summary>
    public class MarketDataRepository_GetActiveMarketDataValuation
    {
        private IMarketDataRepository marketDataRepository;
        private Mock<CanWeFixItContext> mockContext;

        /// <summary>
        /// Setup tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<CanWeFixItContext>();

            var mockLogger = new Mock<ILogger<MarketDataRepository>>();

            marketDataRepository = new MarketDataRepository(mockContext.Object, mockLogger.Object);
        }

        /// <summary>
        /// Test to return active market data valuation
        /// </summary>
        [Test]
        public void InstrumentRepository_ShouldCalculateTotalValuationWithActiveMarketData()
        {
            var expectedTotal = 40;

            var dataSet = new List<MarketData>{
                 new MarketData { Id = 1, DataValue = 10, Active = true },
                 new MarketData { Id = 2, DataValue = 20, Active = false },
                 new MarketData { Id = 3, DataValue = 30, Active = true },
                 new MarketData { Id = 4, DataValue = 40, Active = false }
            }.AsQueryable();

            setContext(dataSet);

            var result = marketDataRepository.getActiveMarketDataValuation();

            Assert.IsTrue(result == expectedTotal);
        }

        /// <summary>
        /// Test to return zero if market data are not available
        /// </summary>
        [Test]
        public void InstrumentRepository_ShouldReturnZeroIfMarketDataAreNotAvailable()
        {
            var expectedTotal = 0;

            var dataSet = new List<MarketData>{}.AsQueryable();

            setContext(dataSet);

            var result = marketDataRepository.getActiveMarketDataValuation();

            Assert.IsTrue(result == expectedTotal);
        }

        /// <summary>
        /// Set context
        /// </summary>
        private void setContext(IQueryable<MarketData> dataSet)
        {
            var mockDbSet = new Mock<DbSet<MarketData>>();

            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.Provider).Returns(dataSet.Provider);
            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.Expression).Returns(dataSet.Expression);
            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.ElementType).Returns(dataSet.ElementType);
            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.GetEnumerator()).Returns(dataSet.GetEnumerator());

            mockContext.Setup(e => e.MarketData).Returns(mockDbSet.Object);
        }
    }
}

