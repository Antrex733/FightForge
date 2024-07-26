namespace FightForge.Controllers
{
    [ApiController]
    [Route("api/Gym/{gymId}/[controller]")]
    [Authorize]
    public class SportController : ControllerBase
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromRoute] int gymId, [FromBody] CreateSportDto dto) 
        {
            var result = await _sportService.Create(gymId, dto);

            return Created($"{gymId}/Sport/{result}", null);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAll([FromRoute] int gymId)
        {
            var result = _sportService.GetAll(gymId);

            return Ok(result);
        }
        [HttpGet("{sportId}")]
        [AllowAnonymous]
        public ActionResult GetOne([FromRoute] int gymId, [FromRoute] int sportId)
        {
            var result = _sportService.GetById(gymId, sportId);

            return Ok(result);
        }
        [HttpPatch("{sportId}")]
        public ActionResult Put([FromRoute] int gymId, [FromRoute] int sportId, [FromBody] UpdateSportDto sportDto)
        {
            _sportService.Update(gymId, sportId, sportDto);

            return NoContent();
        }
        [HttpDelete("{sportId}")]
        public async Task<ActionResult> DeleteOne([FromRoute] int gymId, [FromRoute] int sportId)
        {
            await _sportService.DeleteById(gymId, sportId);

            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAllFromGym([FromRoute] int gymId)
        {
            await _sportService.DeleteAll(gymId);

            return NoContent();
        }
        //wyszukiwanie?
    }
}
