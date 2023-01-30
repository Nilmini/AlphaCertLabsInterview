using System;
namespace CanWeFixItService.Models
{
    /// <summary>
    /// DTO class for the MarketData
    /// </summary>
    public class MarketDataDto : IEquatable<MarketDataDto>
    {
        /// <summary>
        /// Id property
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// DataValue property
        /// </summary>
        public long? DataValue { get; set; }

        /// <summary>
        /// InstrumentId property
        /// </summary>
        public long? InstrumentId { get; set; }

        /// <summary>
        /// Active property
        /// </summary>
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as MarketDataDto);
        }

        public bool Equals(MarketDataDto other)
        {
            return other != null &&
                   Id == other.Id &&
                   DataValue == other.DataValue &&
                   InstrumentId == other.InstrumentId &&
                   Active == other.Active;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DataValue, InstrumentId, Active);
        }
    }
}

