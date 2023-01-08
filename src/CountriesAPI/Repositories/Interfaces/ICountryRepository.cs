using CountriesAPI.Domain.Entities;
using System.Diagnostics.Metrics;

namespace CountriesAPI.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryEntity>> ListAll();

        Task<CountryEntity?> SelectById(Guid id);

        Task<bool> Delete(Guid id);

        Task<bool> InsertOrUpdate(CountryEntity newCountry);

        //Task<CountrySubDivision> AddSubdivision(CountrySubDivision subdivision);

        //Task<IEnumerable<Country>> ListAllWithSubdivisions(); {
    }
}
