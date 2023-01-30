using System;
using System.Linq;
using CanWeFixItRepository.Models;
using Microsoft.Extensions.Logging;

namespace CanWeFixItService.Services
{
    /// <summary>
    /// Class of DatabaseSeedService
    /// </summary>
    public class DatabaseSeedService : IDatabaseSeedService
    {
        private readonly CanWeFixItContext _canWeFixItContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of DatabaseSeedService
        /// </summary>
        /// <param name="canWeFixItContext"> Instance of CanWeFixItContext </param>
        /// <param name="logger"> Instance of Logger </param>
        public DatabaseSeedService(CanWeFixItContext canWeFixItContext, ILogger<DatabaseSeedService> logger)
        {
            _canWeFixItContext = canWeFixItContext;
            _logger = logger;
        }

        /// <summary>
        /// Seed database with data
        /// </summary>
        public void seedDatabase()
        {
            _logger.LogInformation("DatabaseSeedService.seedDatabase() called.");

            var sedols = generateSedols();

            var instruments = generateInstruments(sedols);

            var marketData = generateMarketData(sedols);

            _canWeFixItContext.AddRange(sedols);

            _canWeFixItContext.AddRange(instruments);

            _canWeFixItContext.AddRange(marketData);

            _logger.LogInformation("Started database seeding.");

            _canWeFixItContext.SaveChanges();

            _logger.LogInformation($"Database seeded successfully. " +
                $"{sedols.Count()} rows inserted Sedol table, " +
                $"{instruments.Count()} rows inserted Instrument table and  " +
                $"{marketData.Count()} rows inserted MarketData table.");
        }

        /// <summary>
        /// Generate data for the Sedol table
        /// </summary>
        /// <returns> An array of Sedol entities </returns>
        private Sedol[] generateSedols()
        {
            _logger.LogInformation("DatabaseSeedService.generateSedols() called.");

            Sedol[] sedolRows =
            {
                new Sedol { Id = 1, SedolName = "Sedol1" },
                new Sedol { Id = 2, SedolName = "Sedol2" },
                new Sedol { Id = 3, SedolName = "Sedol3" },
                new Sedol { Id = 4, SedolName = "Sedol4" },
                new Sedol { Id = 5, SedolName = "Sedol5" },
                new Sedol { Id = 6, SedolName = "Sedol6" },
                new Sedol { Id = 7, SedolName = "Sedol7" },
                new Sedol { Id = 8, SedolName = "Sedol8" },
                new Sedol { Id = 9, SedolName = "Sedol9" }
            };

            return sedolRows;

        }

        /// <summary>
        /// Generate data for the Instrument table
        /// </summary>
        /// <param name="sedols"> An array of Sedol entities </param>
        /// <returns> An array of Instrument entities </returns>
        private Instrument[] generateInstruments(Sedol[] sedols)
        {
            _logger.LogInformation("DatabaseSeedService.generateInstruments() called.");

            var sedolsMap = sedols.ToDictionary(p => p.SedolName, p => p);

            Instrument[] instrumentRows =
            {
                new Instrument { Id = 1, Sedol = sedolsMap["Sedol1"], Name = "Name1", Active = false},
                new Instrument { Id = 2, Sedol = sedolsMap["Sedol2"], Name = "Name2", Active = true },
                new Instrument { Id = 3, Sedol = sedolsMap["Sedol3"], Name = "Name3", Active = false },
                new Instrument { Id = 4, Sedol = sedolsMap["Sedol4"], Name = "Name4", Active = true },
                new Instrument { Id = 5, Sedol = sedolsMap["Sedol5"], Name = "Name5", Active = false },
                new Instrument { Id = 6, Name = "Name6", Active = true },
                new Instrument { Id = 7, Sedol = sedolsMap["Sedol7"], Name = "Name7", Active = false },
                new Instrument { Id = 8, Sedol = sedolsMap["Sedol8"], Name = "Name8", Active = true },
                new Instrument { Id = 9, Sedol = sedolsMap["Sedol9"], Name = "Name9", Active = false }
            };
            return instrumentRows;

        }

        /// <summary>
        /// Generate data for the MarketData table
        /// </summary>
        /// <param name="sedols"> An array of Sedol entities </param>
        /// <returns> An array of MarketData entities </returns>
        private MarketData[] generateMarketData(Sedol[] sedols)
        {
            _logger.LogInformation("DatabaseSeedService.generateMarketData() called.");

            var sedolsMap = sedols.ToDictionary(p => p.SedolName, p => p);

            MarketData[] marketDataRows = 
            {
                new MarketData { Id = 1, DataValue=1111, Sedol = sedolsMap["Sedol1"], Active = false },
                new MarketData { Id = 2, DataValue=2222, Sedol = sedolsMap["Sedol2"], Active = true },
                new MarketData { Id = 3, DataValue=3333, Sedol = sedolsMap["Sedol3"], Active = false },
                new MarketData { Id = 4, DataValue=4444, Sedol = sedolsMap["Sedol4"], Active = true },
                new MarketData { Id = 5, DataValue=5555, Sedol = sedolsMap["Sedol5"], Active = false },
                new MarketData { Id = 6, DataValue=6666, Sedol = sedolsMap["Sedol6"], Active = true }
            };

            return marketDataRows;
        }
    }
}

