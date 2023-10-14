using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.HealthCheck.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<MyHealthCheck> _logger;

        public TestController(ILogger<MyHealthCheck> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            throw new Exception("11");
            _logger.LogInformation("11111");
            return Guid.NewGuid().ToString();
        }
    }
}
