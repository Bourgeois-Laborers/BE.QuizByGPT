using AutoMapper;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.QuizByGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizSessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuizSessionService _quizSessionService;

        public QuizSessionController(IMapper mapper, IQuizSessionService quizSessionService)
        {
            _mapper = mapper;
            _quizSessionService = quizSessionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var quizSession = await _quizSessionService.Get(id);

            return Ok(_mapper.Map<QuizSessionModelGetDto>(quizSession));
        }

        [HttpPost("{quizId}")]
        public async Task<IActionResult> Create(Guid quizId)
        {
            if (Guid.TryParse(Request.Cookies["UserSessionId"], out var userSessionId))
            {
                var createdQuizSession = await _quizSessionService.CreateByQuizId(userSessionId, quizId);

                return CreatedAtAction(nameof(Get), new { id = createdQuizSession.Id }, _mapper.Map<QuizSessionModelGetDto>(createdQuizSession));
            }

            return BadRequest("UserSessionId invalid.");
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> AttachUserToQuizSession(Guid id)
        {
            if (Guid.TryParse(Request.Cookies["UserSessionId"], out var userSessionId))
            {
                await _quizSessionService.AttachUserToQuizSession(userSessionId, id);

                return Ok();
            }

            return BadRequest("UserSessionId invalid.");
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateUserReady(Guid id, bool isReady)
        {
            if (Guid.TryParse(Request.Cookies["UserSessionId"], out var userSessionId))
            {
                await _quizSessionService.UpdateUserReady(userSessionId, id, isReady);

                return Ok();
            }

            return BadRequest("UserSessionId invalid.");
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Start(Guid id)
        {
            if (Guid.TryParse(Request.Cookies["UserSessionId"], out var userSessionId))
            {
                await _quizSessionService.Start(userSessionId, id);

                return Ok();
            }

            return BadRequest("UserSessionId invalid.");
        }
    }
}
