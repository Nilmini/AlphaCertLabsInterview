using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CanWeFixItRepository.Repositories
{
    /// <summary>
    /// Class of InstrumentRepository
    /// </summary>
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly CanWeFixItContext _canWeFixItContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of the InstrumentRepository class
        /// </summary>
        /// <param name="canWeFixItContext">Instance of CanWeFixItContext</param>
        /// <param name="logger">Instance of Logger</param>
        public InstrumentRepository(CanWeFixItContext canWeFixItContext, ILogger<InstrumentRepository> logger)
        {
            _canWeFixItContext = canWeFixItContext;
            _logger = logger;
        }

        /// <summary>
        /// Get active instrument entitiy records
        /// </summary>
        /// <returns>Set of instrument entities</returns>
        public IEnumerable<Instrument> getActiveInstruments()
        {
            _logger.LogInformation("InstrumentRepository.getActiveInstruments() called.");

            return _canWeFixItContext.Instruments
                .Where(e => e.Active == true)
                .Include(e => e.Sedol);
        }
    }
}

