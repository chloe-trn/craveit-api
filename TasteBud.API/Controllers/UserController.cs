using Microsoft.AspNetCore.Mvc;
using TasteBud.API.Services.UserService;
using TasteBud.API.ViewModels.Login;
using TasteBud.API.ViewModels.Register;

namespace TasteBud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor of UserController
        // Param: instance of IUserService
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/register
        // Endpoint for user registration
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            // Call UserService to register a new user
            var result = await _userService.Register(registerViewModel);

            // Return result of the registration process in an Ok response
            return Ok(result);
        }

        // POST: api/user/login
        // Endpoint for user login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            // Call UserService to perform the login operation
            var result = await _userService.Login(loginViewModel);

            // Return the result of the login process in an Ok response
            return Ok(result);
        }
    }
}