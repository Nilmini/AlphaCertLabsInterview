using System;
using System.Collections.Generic;
using CanWeFixItRepository.Models;

namespace CanWeFixItRepository.Repositories
{
    /// <summary>
    /// Interface for the InstrumentRepository
    /// </summary>
    public interface IInstrumentRepository
    {
        /// <summary>
        /// Get active instrument entitiy records
        /// </summary>
        /// <returns>Set of instrument entities</returns>
        IEnumerable<Instrument> getActiveInstruments();
    }
}

