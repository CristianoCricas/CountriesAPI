using CountriesAPI.Data;
using CountriesAPI.Repositories;
using CountriesAPI.Repositories.Interfaces;
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


/////////////////
            // Configure PostgreSQL
            builder.Services.AddEntityFrameworkNpgsql()
                .AddDbContext<DbCountriesContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));


            builder.Services.AddScoped<ICountryRepository, CountryRepository>();

////////////////

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}