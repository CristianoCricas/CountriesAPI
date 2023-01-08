using CountriesAPI.Domain.Entities;

namespace CountriesAPI.Domain.Interfaces
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
