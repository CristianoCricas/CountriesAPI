using CountriesAPI.Data;
using CountriesAPI.Domain.Entities;
using CountriesAPI.Enums;
using CountriesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DbCountriesContext _context;

        public CountryRepository(DbCountriesContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(Guid id)
        {
            var delCountry = await SelectById(id);
            if (delCountry != null)
            {
                _context.Countries.Remove(delCountry);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> InsertOrUpdate(CountryEntity newCountry)
        {
            bool result = false;
            DataBaseAction dbAction = newCountry.ValidateDefaultValues();

            if (dbAction == DataBaseAction.Insert) 
            {
                await _context.Countries.AddAsync(newCountry);
                result = true;
            }
            else
            {
                var oldCountry = await SelectById(newCountry.Id);
                if (oldCountry != null)
                {
                    oldCountry.CopyFrom(newCountry);
                    _context.Countries.Update(oldCountry);
                    result = true;
                }
            }

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<CountryEntity>> ListAll()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<CountryEntity?> SelectById(Guid id)
        {
            var country = await _context.Countries.FindAsync(id);
            return country;
        }
    }
}
