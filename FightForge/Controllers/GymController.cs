using Microsoft.AspNetCore.Authorization;

namespace FightForge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;

        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<GymDto>> GetAll()
        {
            var gyms = _gymService.GetAll();

            return Ok(gyms);
        }
        [AllowAnonymous]
        [HttpGet("{gymId}")]
        public ActionResult<GymDto> GetOne([FromRoute] int gymId)
        {
            var gym = _gymService.GetById(gymId);

            return Ok(gym);
        }
    }
}
