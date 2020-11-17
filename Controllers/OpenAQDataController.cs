using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TechTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAQDataController : ControllerBase
    {
        private readonly ILogger<OpenAQDataController> _logger;

        public OpenAQDataController(ILogger<OpenAQDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
