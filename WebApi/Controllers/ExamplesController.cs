using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("Examples")]
    [ApiController]
    [Authorize]
    public class ExamplesController : ControllerBase
    {
        private IExampleService _exampleService;

        public ExamplesController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet("{Id}", Name = "GetExampleById")]
        [ProducesResponseType(200,Type = typeof(Example))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetExample(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Example result = _exampleService.GetExample(Id);

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
        [ProducesResponseType(200, Type = typeof(IQueryable<Example>))]
        [ProducesResponseType(500)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetAll(string sort)
        {
            try
            {
                IQueryable<Example> result = _exampleService.GetExamples(sort);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(200,Type = typeof(string))]
        [ProducesResponseType(500)]
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
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(200, Type = typeof(IQueryable<Example>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public IActionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Search parameter cannot be null or empty.");
            }
            try
            {
                IQueryable<Example> result = _exampleService.Search(name);
                if (result.Count() == 0 || result == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id:int}", Name = "DeleteExample")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        [ProducesResponseType(200, Type = typeof(IQueryable<Example>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Paging(int? pageNumber, int? pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IQueryable<Example> result = _exampleService.Paging(pageNumber, pageSize);

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