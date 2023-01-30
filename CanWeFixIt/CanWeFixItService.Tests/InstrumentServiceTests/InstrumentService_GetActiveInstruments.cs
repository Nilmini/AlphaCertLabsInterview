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

namespace CanWeFixItService.Tests.InstrumentServiceTests
{
    /// <summary>
    /// Test class to test the service method of active instruments
    /// </summary>
    public class InstrumentService_GetActiveInstruments
    {
        private IInstrumentService instrumentService;
        private Mock<IInstrumentRepository> mockInstrumentRepository;

        /// <summary>
        /// Setup tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockInstrumentRepository = new Mock<IInstrumentRepository>();

            var mockLogger = new Mock<ILogger<InstrumentService>>();

            instrumentService = new InstrumentService(mockInstrumentRepository.Object, mockLogger.Object);
        }

        /// <summary>
        /// Test to return active instruments list
        /// </summary>
        [Test]
        public void InstrumentService_ShouldReturnActiveInstruments()
        {
            List<Instrument> instrumentEntities = new List<Instrument>();

            instrumentEntities.Add(new Instrument { Id = 1, Name = "Name1", Sedol = new Sedol { SedolName = "Sedol1" }, Active = true });
            instrumentEntities.Add(new Instrument { Id = 3, Name = "Name3", Active = true });

            mockInstrumentRepository.Setup(e => e.getActiveInstruments()).Returns(instrumentEntities);

            List<InstrumentDto> expectedInstruments = new List<InstrumentDto>();

            expectedInstruments.Add(new InstrumentDto { Id = 1, Name = "Name1", Sedol = "Sedol1", Active = true });
            expectedInstruments.Add(new InstrumentDto { Id = 3, Name = "Name3", Active = true });

            var result = instrumentService.getActiveInstruments();

            Assert.AreEqual(expectedInstruments.Count(), result.Count());

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.IsTrue(result[i].Equals(expectedInstruments[i]));
            }
        }

    }
}

