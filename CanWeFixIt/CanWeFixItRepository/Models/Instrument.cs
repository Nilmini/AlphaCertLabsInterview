using System;
namespace CanWeFixItRepository.Models
{
    /// <summary>
    /// Entity class of Instrument
    /// </summary>
    public class Instrument
    {
        /// <summary>
        /// Id data member
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name data member
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Active data member
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// SedolId data member
        /// </summary>
        public long? SedolId { get; set; }

        /// <summary>
        /// Sedol reference member
        /// </summary>
        public virtual Sedol Sedol { get; set; }
    }
}

