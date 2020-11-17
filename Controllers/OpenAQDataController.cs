using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Model;
using Model.DTOs;
using Service;
using Service.Interfaces;

namespace TechTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAQDataController : ControllerBase
    {
        private readonly ILogger<OpenAQDataController> _logger;
        private readonly IOpenaqDataService _dataService;

        public OpenAQDataController(ILogger<OpenAQDataController> logger, IOpenaqDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;

            // initiliase data:
            _dataService.Initialise();
        }


        [HttpGet("countries")]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _dataService.GetCountries();

            return Ok(countries);
        }

        [HttpGet("countries-cities-locations")]
        [ProducesResponseType(typeof(IEnumerable<CountriesAndCitiesDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountriesAndCities()
        {
            var response = await _dataService.GetCountriesAndCities();

            return Ok(response);
        }

        [HttpGet("city/{cityCode}/location/{locationName}/measurements")]
        [ProducesResponseType(typeof(IEnumerable<LocationMeasurementsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeasurementsForLocation(string cityCode, string locationName)
        {

            if(string.IsNullOrWhiteSpace(cityCode) || string.IsNullOrWhiteSpace(locationName))
            {
                return BadRequest();
            }

            var response = await _dataService.GetCountriesAndCities();

            return Ok(response);
        }
    }
}
