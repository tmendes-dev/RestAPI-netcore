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
                return new List<Example>() { new Example() {ID= 1,Date = DateTime.Now,Available= true,Name="Get Test",Quantity=1 } };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}