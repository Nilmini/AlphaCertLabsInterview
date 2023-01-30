using System;
using System.Collections.Generic;
using System.Linq;
using CanWeFixItRepository.Repositories;
using CanWeFixItService.Models;
using Microsoft.Extensions.Logging;

namespace CanWeFixItService.Services
{
    /// <summary>
    /// Class of InstrumentService
    /// </summary>
    public class InstrumentService : IInstrumentService
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of the class InstrumentService
        /// </summary>
        /// <param name="instrumentRepository"> Instance of IInstrumentRepository </param>
        /// <param name="logger"> Instance of Logger </param>
        public InstrumentService(IInstrumentRepository instrumentRepository, ILogger<InstrumentService> logger)
        {
            _instrumentRepository = instrumentRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get active instruments
        /// </summary>
        /// <returns> A list of InstrumentDto </returns>
        public List<InstrumentDto> getActiveInstruments()
        {
            _logger.LogInformation("InstrumentService.getActiveInstruments() called.");

            return _instrumentRepository.getActiveInstruments()
                .Select(e => new InstrumentDto(){
                    Id = e.Id,
                    Sedol=e.Sedol?.SedolName,
                    Active=e.Active,
                    Name=e.Name })
                .ToList();
        }
    }
}

