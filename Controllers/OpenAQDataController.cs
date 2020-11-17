using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Model;
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
        }



        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var countries = await _dataService.GetCountries();

            if (countries.Any())
            {
                return Ok(countries);
            }

            else
            {
                return NoContent();
            }
        }
    }
}
