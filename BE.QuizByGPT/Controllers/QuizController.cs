using AutoMapper;
using BE.QuizByGPT.BLL.Enums;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using BE.QuizByGPT.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.QuizByGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;

        public QuizController(IMapper mapper, IQuizService quizService)
        {
            _mapper = mapper;
            _quizService = quizService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var quiz = await _quizService.Get(id);

            return Ok(_mapper.Map<QuizModelGetDto>(quiz));
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuizModelPostDto quizPostDto)
        {
            if (Guid.TryParse(Request.Cookies["UserSessionId"], out var userSessionId))
            {
                var createdQuiz = await _quizService.Create(userSessionId, _mapper.Map<QuizModel>(quizPostDto));

                return CreatedAtAction(nameof(Get), new { id = createdQuiz.Id }, _mapper.Map<QuizModelGetDto>(createdQuiz));
            }

            return BadRequest("UserSessionId invalid.");
        }
    }
}
