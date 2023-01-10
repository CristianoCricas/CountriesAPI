using CountriesAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CountriesAPI.Domain.Entities;

namespace CountriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }


        /// <summary>
        /// Default API exposed action to Select ALL Countries on DataBase
        /// </summary>
        /// <returns>JSON with ALL Countries on DataBase</returns>
        [HttpGet] //HTTP GET - Get all
        public async Task<IActionResult> ListAllCountries()
        {
            IEnumerable<CountryEntity> countries = await _countryRepository.ListAll();

            return Ok(countries); //200 OK
        }

        /// <summary>
        /// API exposed action to show Total Countries on DataBase
        /// </summary>
        /// <returns>Total obtained countries</returns>
        [HttpGet("total")]
        public async Task<IActionResult> TotalCountries()
        {
            IEnumerable<CountryEntity> countries = await _countryRepository.ListAll();
            var total = countries.Count();

            return Ok($"Total obtained countries: {total}"); //200 OK
        }

        /// <summary>
        /// API exposed action to Select one Country on DataBase by it's ID
        /// </summary>
        /// <param name="id">ID of the Country that will be selected</param>
        /// <returns>JSON with Country on DataBase</returns>
        [HttpGet("{id}")] //HTTP GET {id} - Get for given Id
        public async Task<IActionResult> SelectCountryById(Guid id)
        {
            CountryEntity? country = await _countryRepository.SelectById(id);

            if (country == null)
                return NotFound(); //404 Not Found

            return Ok(country); //200 OK
        }

        /// <summary>
        /// API exposed action to Register a New Country on DataBase
        /// </summary>
        /// <param name="newCountry">JSON with Country informations (REQUIRED: Name, Alpha2Code, Alpha3Code, NumericCode, Independet )</param>
        /// <returns>JSON with new Country registered and it's ID on DataBase</returns>
        [HttpPost] //HTTP POST - Create new
        public async Task<IActionResult> RegisterCountry([FromBody] CountryEntity newCountry)
        {
            newCountry.Id = Guid.Empty; //clear Guid to Insert a new Country

            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400 Bad Request

            await _countryRepository.InsertOrUpdate(newCountry);
            return Created($"api/countries/{newCountry.Id}", newCountry); //201 Created
        }

        /// <summary>
        /// API exposed action to Update a Country on DataBase
        /// </summary>
        /// <param name="id">ID of the Country that will be updated</param>
        /// <param name="edtCountry">JSON with Country informations (REQUIRED: Name, Alpha2Code, Alpha3Code, NumericCode, Independet )</param>
        /// <returns>204 No Content</returns>
        [HttpPut("{id}")] //HTTP PUT {id} - Update for given Id
        public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] CountryEntity edtCountry)
        {
            edtCountry.Id = id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400 Bad Request

            bool result = await _countryRepository.InsertOrUpdate(edtCountry);
            if (!result)
                return NotFound(edtCountry); //404 Not Found

            return NoContent(); //204 No Content
        }

        /// <summary>
        /// API exposed action to Delete a Country on DataBase
        /// </summary>
        /// <param name="id">ID of the Country that will be deleted</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id}")] //HTTP DELETE {id} - Delete for given Id
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            bool deleteOK = await _countryRepository.Delete(id);
            if (!deleteOK)
                return NotFound($"Country ID: {id}"); //404 Not Found

            return NoContent(); //204 No Content
        }


        /// <summary>
        /// API exposed action to Select ALL Subdivisions of a Country on DataBase
        /// </summary>
        /// <param name="countryId">Country ID of the  Subdivisiosn </param>
        /// <returns>JSON with ALL Subdivisions of a Countries on DataBase</returns>
        [HttpGet("{countryId}/subdivisions")] //HTTP GET - Get all
        public async Task<IActionResult> ListAllSubdivisions([FromRoute] Guid countryId)
        {
            IEnumerable<CountrySubdivisionEntity> subdivisions = await _countryRepository.ListAllSubdivisions(countryId);

            return Ok(subdivisions); //200 OK
        }

        /// <summary>
        /// API exposed action to show Total Subdivisions of Countries on DataBase
        /// </summary>
        /// <returns>Total obtained country subdivisions</returns>
        [HttpGet("{countryId}/subdivisions/total")]
        public async Task<IActionResult> TotalCountrySubdivisions([FromRoute] Guid countryId)
        {
            IEnumerable<CountrySubdivisionEntity> subdivisions = await _countryRepository.ListAllSubdivisions(countryId);
            var total = subdivisions.Count();
            var selectedCountry = await _countryRepository.SelectById(countryId);

            if (selectedCountry == null)
                return NotFound($"Country ID: {countryId}"); //404 Not Found

            return Ok($"Total obtained subdivisions of {selectedCountry.Name}: {total}"); //200 OK
        }

        /// <summary>
        /// API exposed action to Select one Subdivision of Country on DataBase by it's ID
        /// </summary>
        /// <param name="countryId">Country ID of the  Subdivisiosn </param>
        /// <param name="subId">ID of the Subdivision of Country that will be selected</param>
        /// <returns>JSON with Country Subdivision on DataBase</returns>
        [HttpGet("{countryId}/subdivisions/{subId}")] //HTTP GET {subId} - Get for given Id
        public async Task<IActionResult> SelectSubdivisionById([FromRoute] Guid countryId, Guid subId)
        {
            CountrySubdivisionEntity? subdivision = await _countryRepository.SelectSubdivisionById(countryId, subId);

            if (subdivision == null)
                return NotFound(); //404 Not Found

            return Ok(subdivision); //200 OK
        }

        /// <summary>
        /// API exposed action to Register a new Country Subdivision on DataBase
        /// </summary>
        /// <param name="countryId">Country ID of the new Subdivision</param>
        /// <param name="newSubdivision">JSON with Subdivision informations (REQUIRED: Name, Category, SubCode )</param>
        /// <returns>JSON with new Subdivision registered and it's ID on DataBase</returns>
        [HttpPost("{countryId}/subdivisions")] //HTTP POST - Create new
        public async Task<IActionResult> RegisterCountrySubdivision([FromRoute] Guid countryId, [FromBody] CountrySubdivisionEntity newSubdivision)
        {
            newSubdivision.Id = Guid.Empty; //clear Guid to Insert a new Subdivision
            newSubdivision.CountryId = countryId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400 Bad Request

            string response = await _countryRepository.InsertOrUpdateSubdivision(newSubdivision);
            if (!string.IsNullOrEmpty(response))
                return NotFound(response); //404 Not Found

            return Created($"api/countries/{newSubdivision.CountryId}/subdivisons/{newSubdivision.Id}", newSubdivision); //201 Created
        }

        /// <summary>
        /// API exposed action to Update a Country Subdivision on DataBase
        /// </summary>
        /// <param name="countryId">Country ID of the Subdivision that will be updated</param>
        /// <param name="subId">ID of the Subdivision of Country that will be updated</param>
        /// <param name="edtSubdivision">JSON with Subdivision informations (REQUIRED: Name, Category, SubCode )</param>
        /// <returns>204 No Content</returns>
        [HttpPut("{countryId}/subdivisions/{id}")] //HTTP PUT {subId} - Update for given Id
        public async Task<IActionResult> UpdateSubdivision([FromRoute] Guid countryId, Guid subId, [FromBody] CountrySubdivisionEntity edtSubdivision)
        {
            edtSubdivision.Id = subId;
            edtSubdivision.CountryId = countryId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400 Bad Request

            string response = await _countryRepository.InsertOrUpdateSubdivision(edtSubdivision);
            if (!string.IsNullOrEmpty(response))
                return NotFound(response); //404 Not Found

            return NoContent(); //204 No Content
        }

        /// <summary>
        /// API exposed action to Delete a Subdivision of a Country on DataBase
        /// </summary>
        /// <param name="countryId">Country ID of the  Subdivisiosn </param>
        /// <param name="subId">ID of the Subdivision of Country that will be deleted</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{countryId}/subdivisions/{subId}")] //HTTP DELETE {subId} - Delete for given Id
        public async Task<IActionResult> DeleteCountry([FromRoute] Guid countryId, Guid subId)
        {
            bool deleteOK = await _countryRepository.DeleteSubdivision(countryId, subId);
            if (!deleteOK)
                return NotFound($"Subdivision ID: {subId}"); //404 Not Found

            return NoContent(); //204 No Content
        }

    }
}
