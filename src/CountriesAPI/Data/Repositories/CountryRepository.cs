using CountriesAPI.Domain.Entities;
using CountriesAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        protected readonly DbCountriesContext _context;

        public CountryRepository(DbCountriesContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<bool> InsertOrUpdate(CountryEntity newCountry)
        {
            bool result = false;

            if (newCountry.IsInserting())
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

        /// <inheritdoc />
        public async Task<IEnumerable<CountryEntity>> ListAll()
        {
            return await _context.Countries.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<CountryEntity?> SelectById(Guid id)
        {
            var country = await _context.Countries.FindAsync(id);
            return country;
        }
    }
}
