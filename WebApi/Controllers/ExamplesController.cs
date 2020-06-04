using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("Examples")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        private IExampleService _exampleService;

        public ExamplesController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet("{Id}", Name = "GetExampleById")]
        public IActionResult GetExample(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = _exampleService.GetExample(Id);

                return Ok(result);
            }
            catch (ApplicationException appException)
            {
                return NotFound(appException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="sort">Optional Parameter Sort: You can sort by date using string parameter = asc or desc</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(string sort)
        {
            try
            {
                var result = _exampleService.GetExamples(sort);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Example example)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _exampleService.CreateExample(example);

                return Ok("Example created sucessfuly!");
            }
            catch (ApplicationException appException)
            {
                return NotFound(appException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] Example example)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _exampleService.UpdateExample(example);

                return Ok("Example updated sucessfuly!");
            }
            catch (ApplicationException appException)
            {
                return NotFound(appException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id:int}",Name = "DeleteExample")]
        public IActionResult Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _exampleService.Delete(Id);

                return Ok("Example deleted sucessfuly!");
            }
            catch (ApplicationException appException)
            {
                return NotFound(appException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Paging")]
        public IActionResult Paging(int? pageNumber, int? pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IQueryable<Example> result =  _exampleService.Paging(pageNumber,pageSize);

                return Ok();
            }
            catch (ApplicationException appException)
            {
                return NotFound(appException.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}