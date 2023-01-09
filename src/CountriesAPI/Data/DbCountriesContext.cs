using CountriesAPI.Data.Map;
using CountriesAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.Data
{
    public class DbCountriesContext : DbContext
    {
        /// <summary>
        /// Starts the DataBase Context
        /// </summary>
        /// <param name="modelBuilder"></param>
        public DbCountriesContext(DbContextOptions<DbCountriesContext> options)
            : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public DbSet<CountryEntity> Countries { get; set; }

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
