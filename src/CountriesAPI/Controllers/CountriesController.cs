using CountriesAPI.Repositories.Interfaces;
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


        [HttpGet]
        public async Task<IActionResult> ListAllCountries()
        {
            IEnumerable<CountryEntity> countries = await _countryRepository.ListAll();

            return Ok(countries); //200 OK
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> SelectCountryById(Guid id)
        {
            CountryEntity? country = await _countryRepository.SelectById(id);

            if (country == null)
                return NotFound(); //404 Not Found

            return Ok(country); //200 OK
        }


        [HttpPost]
        public async Task<IActionResult> RegisterCountry([FromBody] CountryEntity newCountry)
        {
            newCountry.Id = Guid.Empty; //clear Guid to Insert a new Country
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400 Bad Request

            await _countryRepository.InsertOrUpdate(newCountry);
            return Created($"api/countries/{newCountry.Id}", newCountry); //201 Created
        }


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
