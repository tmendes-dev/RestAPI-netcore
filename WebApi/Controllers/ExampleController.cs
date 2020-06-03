using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("Example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {

        [HttpGet]
        [Route("GetExamples")]
        public List<Example> GetExamples()
        {
            try
            {
                return new List<Example>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}