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
        public async Task<bool> DeleteSubdivision(Guid countryId, Guid subId)
        {
            var delSubdivision = await SelectSubdivisionById(countryId, subId);
            if (delSubdivision != null)
            {
                _context.Subdivisions.Remove(delSubdivision);
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
        public async Task<string> InsertOrUpdateSubdivision(CountrySubdivisionEntity newSubdivision)
        {
            var result = $"Subdivision not found. ID: {newSubdivision.Id}";

            //Check if the Country Exists
            var country = await SelectById(newSubdivision.CountryId);
            if (country == null)
                result = $"Country not found. ID: {newSubdivision.CountryId}";


            if (newSubdivision.IsInserting())
            {
                await _context.Subdivisions.AddAsync(newSubdivision);
                result = string.Empty;
            }
            else
            {
                var oldSubdivision = await SelectSubdivisionById(newSubdivision.CountryId, newSubdivision.Id);
                if (oldSubdivision != null)
                {
                    oldSubdivision.CopyFrom(newSubdivision);
                    _context.Subdivisions.Update(oldSubdivision);
                    result = string.Empty;
                }
            }
            await _context.SaveChangesAsync();

            return result;
        }


        /// <inheritdoc />
        public async Task<IEnumerable<CountryEntity>> ListAll(string? name, string? alpha2Code, string? alpha3Code)
        {
            //handling possibly null parameters
            name ??= string.Empty;
            alpha2Code ??= string.Empty;
            alpha3Code ??= string.Empty;


            return await _context.Countries.Where(
                    c => c.Name.ToUpper().Contains(name.ToUpper()) //ToUpper: case InSensitive only on NAME
                    && c.Alpha2Code.Contains(alpha2Code)
                    && c.Alpha3Code.Contains(alpha3Code))
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CountrySubdivisionEntity>> ListAllSubdivisions(Guid countryId)
        {
            return await _context.Subdivisions.Where(s => s.CountryId == countryId).ToListAsync();
        }


        /// <inheritdoc />
        public async Task<IEnumerable<CountryEntity>> SearchCountriesByIds(IEnumerable<Guid> ids)
        {
            return await _context.Countries.Where(c => ids.Contains(c.Id)).ToListAsync(); //where c.Id IN (ids)
        }
        
        /// <inheritdoc />
        public async Task<IEnumerable<CountrySubdivisionEntity>> SearchSubdivisionsByIds(Guid countryId, IEnumerable<Guid> subIds)
        {
            return await _context.Subdivisions.Where(s => s.CountryId == countryId).Where(s => subIds.Contains(s.Id)).ToListAsync(); //where s.Id IN (ids)
        }


        /// <inheritdoc />
        public async Task<CountryEntity?> SelectById(Guid id)
        {
            var country = await _context.Countries.FindAsync(id);
            return country;
        }

        /// <inheritdoc />
        public async Task<CountrySubdivisionEntity?> SelectSubdivisionById(Guid countryId, Guid subId)
        {
            var subdivision = await _context.Subdivisions.Where(s => s.CountryId == countryId).FirstOrDefaultAsync(s => s.Id == subId);
            return subdivision;
        }


    }
}
