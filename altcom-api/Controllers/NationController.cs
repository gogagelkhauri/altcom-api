using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace altcom_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationController : ControllerBase
    {
        private readonly INationService _nationService;
        public NationController(INationService nationService)
        {
            _nationService = nationService;
        }


        [HttpGet("Nations")]
        public async Task<IActionResult> Nations()
        {
            var result = await _nationService.GetNations();
            return Ok(result);
        }
    }
}
