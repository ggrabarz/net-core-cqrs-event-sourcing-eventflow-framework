using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreEventFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok("Hello world");
        }
    }
}
