using FightForge.DTOs;
using FightForge.Services.Interfaces;

namespace FightForge.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto dto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userService.RegisterUser(dto);

            return Ok();
        }
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto dto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = _userService.GenerateJwt(dto);

            return Ok(token);
        }
    }
}
