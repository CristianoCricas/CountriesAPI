using CountriesAPI.Data.Map;
using CountriesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.Data
{
    public class DbCountriesContext : DbContext
    {
        public DbCountriesContext(DbContextOptions<DbCountriesContext> options)
            : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public DbSet<CountryEntity> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CountryMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
