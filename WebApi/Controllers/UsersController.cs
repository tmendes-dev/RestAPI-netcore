using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("Users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                User user = _userRepository.Authenticate(model.Username, model.Password);

                if (user == null)
                {
                    return BadRequest(new { Message = "Username or password is incorrect" });
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}