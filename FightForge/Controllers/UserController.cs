namespace FightForge.Controllers
{
    [ApiController]
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
            _userService.RegisterUser(dto);

            return Ok();
        }
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDto dto) 
        {
            var token = _userService.GenerateJwt(dto);

            return Ok(token);
        }
        [HttpPatch("{userId}/role")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RoleChange([FromRoute] int userId)
        {
            await _userService.RoleChange(userId);

            return NoContent();
        }
    }
}
