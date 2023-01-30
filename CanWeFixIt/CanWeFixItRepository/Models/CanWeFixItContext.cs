using System;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CanWeFixItRepository.Models
{
    /// <summary>
    /// Database Context class 
    /// </summary>
    public class CanWeFixItContext : DbContext
    {
        public CanWeFixItContext(DbContextOptions<CanWeFixItContext> options)
            : base(options)
        {
        }

        public CanWeFixItContext() { }

        public virtual DbSet<Sedol> Sedols { get; set; }

        public virtual DbSet<Instrument> Instruments { get; set; }

        public virtual DbSet<MarketData> MarketData { get; set; }
    }
}

