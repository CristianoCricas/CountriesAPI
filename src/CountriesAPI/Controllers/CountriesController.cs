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
        /// Default API exposed action to Select ALL Countries on DataBase by it's ID
        /// </summary>
        /// <returns>JSON with ALL Countries on DataBase</returns>
        [HttpGet]
        public async Task<IActionResult> ListAllCountries()
        {
            IEnumerable<CountryEntity> countries = await _countryRepository.ListAll();

            return Ok(countries); //200 OK
        }


        /// <summary>
        /// Exposed API action to Select one Country on DataBase by it's ID
        /// </summary>
        /// <param name="id">ID of the Country that will be selected</param>
        /// <returns>JSON with Country on DataBase</returns>
        [HttpGet("{id}")]
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
        /// <param name="newCountry">JSON with Country informatios (REQUIRED: Name, Alpha2Code, Alpha3Code, NumericCode, Independet )</param>
        /// <returns>JSON with new Country registered and it's ID on DataBase</returns>
        [HttpPost]
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
        /// <param name="newCountry">JSON with Country informatios (REQUIRED: ID, Name, Alpha2Code, Alpha3Code, NumericCode, Independet )</param>
        /// <returns>204 No Content</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryEntity edtCountry)
        {
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            bool deleteOK = await _countryRepository.Delete(id);
            if (!deleteOK)
                return NotFound(id); //404 Not Found

            return NoContent(); //204 No Content
        }
    }
}
