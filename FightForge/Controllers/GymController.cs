
namespace FightForge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "owner, admin")]
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
        [HttpPost]
        public async Task<ActionResult> CreateGym([FromBody]CreateGymDto dto)
        {
            await _gymService.Create(dto);

            return Created();
        }
        [HttpPatch("{gymId}")]
        public async Task<IActionResult> PatchGym([FromRoute] int gymId, [FromBody] UpdateGymDto dto)
        {
            await _gymService.Patch(gymId, dto);

            return NoContent();
        }
        [HttpDelete("{gymId}")]
        public async Task<ActionResult> DeleteGym([FromRoute] int gymId)
        {
            await _gymService.Delete(gymId);

            return NoContent();
        }
    }
}
