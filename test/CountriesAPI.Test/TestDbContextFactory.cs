using CountriesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CountriesAPI.Test
{

    public class TestDbContextFactory : IDesignTimeDbContextFactory<DbCountriesContext>
    {
        public DbCountriesContext CreateDbContext(string[] args)
        {
            var options = GetPosterrContextOptions();
            if (options == null)
                throw new Exception("Cannot create ContextOption for DbContext");

            return new DbCountriesContext(options);
        }

        public static DbContextOptions GetPosterrContextOptions()
        {
            var builder = new DbContextOptionsBuilder<DbCountriesContext>();
            builder.EnableSensitiveDataLogging();
            builder.EnableDetailedErrors();
            
            builder.UseNpgsql("Server=localhost;Port=5432;Database=countries_db_TEST;User Id=postgres;Password=Cric@s"); //database countries_db_TEST

            return builder.Options;
        }
    }
}

