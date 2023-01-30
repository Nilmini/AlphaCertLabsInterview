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
    /// Test class to test repository method of the active market data with sedol
    /// </summary>
    public class MarketDataRepository_GetActiveMarketDataWithSedol
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
        /// Test to return active market data with sedol
        /// </summary>
        [Test]
        public void InstrumentRepository_ShouldFilterActiveMarketDataWithSedol()
        {
            var data = new List<MarketData>{
                 new MarketData { Id = 1, DataValue = 1234, Active = true, Sedol = new Sedol{ Instrument = new Instrument { Id = 1 } } },
                 new MarketData { Id = 2, DataValue = 1234, Active = true, Sedol = new Sedol{ Instrument = null } },
                 new MarketData { Id = 3, DataValue = 1234, Active = false, Sedol = new Sedol{ Instrument = new Instrument { Id = 2 } } },
                 new MarketData { Id = 4, DataValue = 1234, Active = false, Sedol = null }
            };

            var dataSet = data.AsQueryable();

            var mockDbSet = new Mock<DbSet<MarketData>>();

            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.Provider).Returns(dataSet.Provider);
            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.Expression).Returns(dataSet.Expression);
            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.ElementType).Returns(dataSet.ElementType);
            mockDbSet.As<IQueryable<MarketData>>().Setup(e => e.GetEnumerator()).Returns(dataSet.GetEnumerator());

            mockContext.Setup(e => e.MarketData).Returns(mockDbSet.Object);

            var expectedData = data[0];

            var result = marketDataRepository.getActiveMarketDataWithSedol();

            Assert.IsTrue(result.Count() == 1);

            var firstResult = result.First();

            Assert.IsTrue(firstResult.Id == expectedData.Id && firstResult.Sedol.Instrument.Id == expectedData.Sedol.Instrument.Id);
        }
    }

}
