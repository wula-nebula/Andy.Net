using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PublishController : Controller
    {
        //private readonly ICapPublisher _capBus;
        //public PublishController(ICapPublisher capPublisher)
        //{
        //    _capBus = capPublisher;
        //}
        //public IActionResult Send()
        //{
        //    _capBus.Publish("template.services.time", DateTime.Now);
        //    return Ok();
        //}

    }
}
