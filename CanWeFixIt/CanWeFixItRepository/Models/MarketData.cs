namespace CanWeFixItRepository.Models
{
    /// <summary>
    /// Entity class of MarketData
    /// </summary>
    public class MarketData
    {
        /// <summary>
        /// Id data member
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// DataValue data member
        /// </summary>
        public long? DataValue { get; set; }

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
        public Sedol Sedol { get; set; }
    }   
}

