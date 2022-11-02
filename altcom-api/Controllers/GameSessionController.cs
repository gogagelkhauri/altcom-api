using altcom_api.Helper;
using Core.DTO;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace altcom_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionController : ControllerBase
    {
        private readonly IGameSessionService _gameSessionService;
        public GameSessionController(IGameSessionService gameSessionService)
        {
            _gameSessionService = gameSessionService;
        }

        [HttpPost("StartSession")]
        public async Task<IActionResult> StartSession()
        {
            var result = await _gameSessionService.StartSession();
            return Ok(result);
        }

        [HttpGet("GetRandomImages")]
        public async Task<IActionResult> GetRandomImages()
        {
            var result = await _gameSessionService.GetRandomImages();
            return Ok(result);
        }

        [HttpPost("SubmitAnswer")]
        public async Task<IActionResult> SubmitAnswer([FromBody] SubmitAnswerRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetModelStateErrors());
            }
            await _gameSessionService.SubmitAnswer(dto);
            return Ok();
        }

        [HttpGet("GetGameSessionPoints")]
        public IActionResult GetGameSessionPoints(int gameSessionId)
        {
            var result = _gameSessionService.GetGameSessionPoints(gameSessionId);
            return Ok(result);
        }

    }
}
