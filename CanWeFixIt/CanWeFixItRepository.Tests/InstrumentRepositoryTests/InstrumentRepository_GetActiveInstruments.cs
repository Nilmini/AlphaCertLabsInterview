using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Models;
using CanWeFixItRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CanWeFixItService.Tests.RepositoryTests.InstrumentRepositoryTests
{
    /// <summary>
    /// Test class to test the repository method of active instruments
    /// </summary>
    public class InstrumentRepository_GetActiveInstruments
    {
        private IInstrumentRepository instrumentRepository;
        private Mock<CanWeFixItContext> mockContext;

        /// <summary>
        /// Setup tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<CanWeFixItContext>();

            var mockLogger = new Mock<ILogger<InstrumentRepository>>();

            instrumentRepository = new InstrumentRepository(mockContext.Object, mockLogger.Object);
        }

        /// <summary>
        /// Test to return active instruments list
        /// </summary>
        [Test]
        public void InstrumentRepository_ShouldFilterActiveValues()
        {
            var data = new List<Instrument>{
                 new Instrument { Name = "Instrument 1", Id = 1, Active = true },
                 new Instrument { Name = "Instrument 2", Id = 2, Active = false },
                 new Instrument {
                     Name = "Instrument 3",
                     Id = 3, Active = true,
                     Sedol = new Sedol { SedolName = "Sedol 1", Id = 1 } }
            };

            var dataSet = data.AsQueryable();

            var mockDbSet = new Mock<DbSet<Instrument>>();

            mockDbSet.As<IQueryable<Instrument>>().Setup(e => e.Provider).Returns(dataSet.Provider);
            mockDbSet.As<IQueryable<Instrument>>().Setup(e => e.Expression).Returns(dataSet.Expression);
            mockDbSet.As<IQueryable<Instrument>>().Setup(e => e.ElementType).Returns(dataSet.ElementType);
            mockDbSet.As<IQueryable<Instrument>>().Setup(e => e.GetEnumerator()).Returns(dataSet.GetEnumerator());

            var expectedData = new List<Instrument> { data[0], data[2] };

            mockContext.Setup(e => e.Instruments).Returns(mockDbSet.Object);

            var result = instrumentRepository.getActiveInstruments().ToList();

            Assert.IsTrue(result.Count == expectedData.Count);

            for (var i = 0; i < result.Count; i++)
            {
                Assert.IsTrue(result[i].Id == expectedData[i].Id);

                if (expectedData[i].Sedol != null)
                {
                    Assert.IsTrue(result[i].Sedol.Id == expectedData[i].Sedol.Id);
                }
            }
        }
    }
}
