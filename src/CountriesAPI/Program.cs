using CountriesAPI.Data;
using CountriesAPI.Data.Repositories;
using CountriesAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configure PostgreSQL
            ConfigureDataBaseAndScopes(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //This webAPI UseSwagger for Production to be accessed on Docker Container
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction()) 
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


        /// <summary>
        /// Configure DataBase Connection and it's scope (the dependecys)
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureDataBaseAndScopes(WebApplicationBuilder builder)
        {
            builder.Services.AddEntityFrameworkNpgsql()
                .AddDbContext<DbCountriesContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
        }
    }
}