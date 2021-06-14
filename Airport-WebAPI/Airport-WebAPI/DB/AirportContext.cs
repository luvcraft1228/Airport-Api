using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport_WebAPI.DB
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(x => x.IATACode);

            });
        }
    }
    
}
