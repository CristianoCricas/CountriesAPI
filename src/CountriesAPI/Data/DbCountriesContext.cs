using CountriesAPI.Data.Map;
using CountriesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.Data
{
    public class DbCountriesContext : DbContext
    {
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CountrySubdivisionEntity> Subdivisions { get; set; }


        /// <summary>
        /// Starts the DataBase Context
        /// </summary>
        public DbCountriesContext(DbContextOptions options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        /// <summary>
        /// Used to modeling Domain Entities to EntityFramework
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CountryMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
