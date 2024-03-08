using BE.QuizByGPT.BLL.Enums;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using BE.QuizByGPT.Interfaces;

namespace BE.QuizByGPT.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        
        public QuizService(IQuizRepository quizRepository, IUserSessionRepository userSessionRepository)
        {
            _quizRepository = quizRepository;
            _userSessionRepository = userSessionRepository;
        }

        public async Task<QuizModel> Get(Guid id)
        {
            var quiz = await _quizRepository.GetAsync(id);
            return quiz ?? throw new Exception("Quiz not found.");
        }

        public async Task<QuizModel> GetExtended(Guid id)
        {
            var quiz = await _quizRepository.GetExtendedAsync(id);
            return quiz ?? throw new Exception("Quiz not found.");
        }

        public async Task<QuizModel> Create(Guid userSessionId, QuizModel quiz)
        {
            var userSession = await _userSessionRepository.GetAsync(userSessionId);
            if (userSession == null)
            {
                throw new Exception("UserSession not found.");
            }
            
            quiz.CreatedBy = userSession.Id;
            quiz.Status = QuizStatusEnum.Draft;

            var createdQuiz = await _quizRepository.CreateAsync(quiz);

            return createdQuiz;
        }
    }
}
