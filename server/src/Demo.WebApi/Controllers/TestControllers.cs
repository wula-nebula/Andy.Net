using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestControllers : ControllerBase
    {
        private readonly ILogger<TestControllers> _logger;

        public TestControllers(ILogger<TestControllers> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<bool> Receive(TestRequest request)
        {
            using StreamWriter file = new("logs.txt", append: true);
            await file.WriteLineAsync($"{request.name}耗时：{(DateTime.Now - request.time).TotalMilliseconds}");
            return true;
        }
    }
    public class TestRequest
    {
        public string name { get; set; }
        public DateTime time { get; set; }
    }
}
