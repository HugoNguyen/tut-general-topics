using Microsoft.AspNetCore.Mvc;

namespace proj.sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
#pragma warning disable CA1848 // Use the LoggerMessage delegates
            _logger.LogInformation("This is test");
#pragma warning restore CA1848 // Use the LoggerMessage delegates
            return Ok("This is test");
        }
    }
}
