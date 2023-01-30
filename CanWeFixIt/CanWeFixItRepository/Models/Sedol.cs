using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CanWeFixItRepository.Models
{
    /// <summary>
    /// Entity class of Sedol
    /// </summary>
    public class Sedol
    {
        /// <summary>
        /// Id data member
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// SedolName data member
        /// </summary>
        public string SedolName { get; set; }

        /// <summary>
        /// Instrument reference member
        /// </summary>
        public virtual Instrument Instrument { get; set; }

        /// <summary>
        /// MarketData reference member collection
        /// </summary>
        public ICollection<MarketData> MarketData { get; set; }
    }
}

