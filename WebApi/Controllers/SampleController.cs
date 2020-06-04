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
    [Route("Samples")]
    [ApiController]
    [Authorize]
    public class SampleController : ControllerBase
    {
        private ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet("{Id}", Name = "GetExampleById")]
        [ProducesResponseType(200,Type = typeof(Sample))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Sample result = _sampleService.Get(Id);

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
        [ProducesResponseType(200, Type = typeof(IQueryable<Sample>))]
        [ProducesResponseType(500)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetAll(string sort)
        {
            try
            {
                IQueryable<Sample> result = _sampleService.GetAll(sort);

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
        public IActionResult Create([FromBody] Sample example)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _sampleService.Create(example);

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
        public IActionResult Update([FromBody] Sample example)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _sampleService.Update(example);

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
        [ProducesResponseType(200, Type = typeof(IQueryable<Sample>))]
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
                IQueryable<Sample> result = _sampleService.Search(name);
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
                _sampleService.Delete(Id);

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
        [ProducesResponseType(200, Type = typeof(IQueryable<Sample>))]
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
                IQueryable<Sample> result = _sampleService.Paging(pageNumber, pageSize);

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