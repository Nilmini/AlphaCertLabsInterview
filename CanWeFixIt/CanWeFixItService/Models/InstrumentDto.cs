using System;
namespace CanWeFixItService.Models
{
    /// <summary>
    /// DTO class for the Instrument
    /// </summary>
    public class InstrumentDto : IEquatable<InstrumentDto>
    {
        /// <summary>
        /// Id property
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Sedol property
        /// </summary>
        public string Sedol { get; set; }

        /// <summary>
        /// Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Active property
        /// </summary>
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as InstrumentDto);
        }

        public bool Equals(InstrumentDto other)
        {
            return other != null &&
                   Id == other.Id &&
                   Sedol == other.Sedol &&
                   Name == other.Name &&
                   Active == other.Active;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Sedol, Name, Active);
        }
    }
}

