using System;
using System.Collections.Generic;
using CanWeFixItService.Models;

namespace CanWeFixItService.Services
{
    /// <summary>
    /// Interface for the InstrumentService class
    /// </summary>
    public interface IInstrumentService
    {
        /// <summary>
        /// Get active instruments
        /// </summary>
        /// <returns> A list of InstrumentDto </returns>
        List<InstrumentDto> getActiveInstruments();
    }
}

