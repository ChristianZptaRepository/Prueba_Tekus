using Microsoft.AspNetCore.Mvc;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces;

namespace Tekus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CountryDto>), 200)]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _countryService.GetCountriesAsync();
            return Ok(countries);
        }
    }
}
