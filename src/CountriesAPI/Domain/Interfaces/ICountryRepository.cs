using CountriesAPI.Domain.Entities;

namespace CountriesAPI.Domain.Interfaces
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Select ALL Countries on DataBase
        /// </summary>
        /// <returns>LIST with ALL Countries on DataBase</returns>
        Task<IEnumerable<CountryEntity>> ListAll(string? name, string? alpha2Code, string? alpha3Code);

        /// <summary>
        /// Select ALL SubDivisions of a Country on DataBase by Country ID
        /// </summary>
        /// <returns>LIST with ALL SubDivisions of a Countries on DataBase</returns>
        Task<IEnumerable<CountrySubdivisionEntity>> ListAllSubdivisions(Guid countryId);


        /// <summary>
        /// Select "N" Countries on DataBase by it's IDs
        /// </summary>
        /// <returns>LIST "N" Countries on DataBase</returns>
        Task<IEnumerable<CountryEntity>> SearchCountriesByIds(IEnumerable<Guid> ids);

        /// <summary>
        /// Select "N" SubDivisions of a Country on DataBase by it's IDs
        /// </summary>
        /// <returns>LIST "N" SubDivisions on DataBase</returns>
        Task<IEnumerable<CountrySubdivisionEntity>> SearchSubdivisionsByIds(Guid countryId, IEnumerable<Guid> subIds);


        /// <summary>
        /// Select one Country on DataBase by it's ID
        /// </summary>
        /// <param name="id">ID of the Country that will be selected</param>
        /// <returns>ENTITY with Country on DataBase</returns>
        Task<CountryEntity?> SelectById(Guid id);

        /// <summary>
        /// Select one Country Subdivision on DataBase by it's ID
        /// </summary>
        /// <param name="subId">ID of the Subdivision that will be selected</param>
        /// <returns>ENTITY with Country on DataBase</returns>
        Task<CountrySubdivisionEntity?> SelectSubdivisionById(Guid countryId, Guid subId);


        /// <summary>
        /// Delete a Country on DataBase
        /// </summary>
        /// <param name="id">ID of the Country that will be deleted</param>
        /// <returns>Action confirmation</returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// Delete a Country Subdivision on DataBase
        /// </summary>
        /// <param name="subId">ID of the Subdivision that will be deleted</param>
        /// <returns>Action confirmation</returns>
        Task<bool> DeleteSubdivision(Guid countryId, Guid subId);


        /// <summary>
        /// Register or Update a Country on DataBase
        /// </summary>
        /// <param name="newCountry">ENTITY with Country informations
        /// <br> REQUIRED: Name, Alpha2Code, Alpha3Code, NumericCode, Independet </br>
        /// <br> OPTIONAL: ID (to UPDATE an existing register)</br>
        /// </param>
        /// <returns>Action confirmation</returns>
        Task<bool> InsertOrUpdate(CountryEntity newCountry);

        /// <summary>
        /// Register or Update a Country Subdivision on DataBase
        /// </summary>
        /// <param name="newSubdivision">ENTITY with Country Subdivision informations
        /// <br> REQUIRED: Country ID, Name, Category, SubCode </br>
        /// <br> OPTIONAL: Subdivision ID (to UPDATE an existing register)</br>
        /// </param>
        /// <returns>Action confirmation</returns>
        Task<string> InsertOrUpdateSubdivision(CountrySubdivisionEntity newSubdivision);

        //Task<IEnumerable<Country>> ListAllWithSubdivisions(); {
    }
}
