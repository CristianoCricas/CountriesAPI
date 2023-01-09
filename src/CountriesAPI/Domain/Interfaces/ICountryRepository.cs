using CountriesAPI.Domain.Entities;

namespace CountriesAPI.Domain.Interfaces
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Select ALL Countries on DataBase by it's ID
        /// </summary>
        /// <returns>LIST with ALL Countries on DataBase</returns>
        Task<IEnumerable<CountryEntity>> ListAll();

        /// <summary>
        /// Select one Country on DataBase by it's ID
        /// </summary>
        /// <param name="id">ID of the Country that will be selected</param>
        /// <returns>ENTITY with Country on DataBase</returns>
        Task<CountryEntity?> SelectById(Guid id);

        /// <summary>
        /// Delete a Country on DataBase
        /// </summary>
        /// <param name="id">ID of the Country that will be deleted</param>
        /// <returns>Action confirmation</returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// Register or Update a Country on DataBase
        /// </summary>
        /// <param name="newCountry">ENTITY with Country informatios
        /// <br> REQUIRED: Name, Alpha2Code, Alpha3Code, NumericCode, Independet </br>
        /// <br> OPTIONAL: ID (to UPDATE an existing register)</br>
        /// </param>
        /// <returns>Action confirmation</returns>
        Task<bool> InsertOrUpdate(CountryEntity newCountry);

        //Task<CountrySubDivision> AddSubdivision(CountrySubDivision subdivision);

        //Task<IEnumerable<Country>> ListAllWithSubdivisions(); {
    }
}
