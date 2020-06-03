using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("Example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet("{Id:int}")]
        [Route("Get")]
        public IActionResult Get(int Id)
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

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _exampleService.GetExamples();

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

        [HttpDelete("{Id:int}")]
        [Route("Delete")]
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
            {m
                return StatusCode(500);
            }
        }
    }
}