using System;

namespace CanWeFixItService
{
    /// <summary>
    /// DTO class for the MarketValuation
    /// </summary>
    public class MarketValuationDto : IEquatable<MarketValuationDto>
    {
        /// <summary>
        /// name property
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// total property
        /// </summary>
        public long total { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as MarketValuationDto);
        }

        public bool Equals(MarketValuationDto other)
        {
            return other != null &&
                   name == other.name &&
                   total == other.total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, total);
        }
    }
}