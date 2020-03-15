using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OptivumParser.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public string Get([FromQuery]string item)
        {
            if (String.IsNullOrEmpty(item))
            {
                return "Hello World!";
            }
            else
            {
                return $"Hello {item}!";
            }
        }
    }
}
