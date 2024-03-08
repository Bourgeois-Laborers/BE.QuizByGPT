using BE.QuizByGPT.GPT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.QuizByGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IGptService _gptService;

        public QuestionController(IGptService gptService)
        {
            _gptService = gptService;
        }

        [HttpGet("[action]/{quizSessionId}")]
        public async Task<IActionResult> GenerateNext(Guid quizSessionId, int answersCount = 4, int correctAnswersCount = 1)
        {
            var question = await _gptService.GetNextQuestionAsync(quizSessionId, answersCount, correctAnswersCount);

            return Ok(question);
        }
    }
}
