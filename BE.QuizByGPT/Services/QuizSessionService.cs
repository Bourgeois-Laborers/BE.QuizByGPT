using BE.QuizByGPT.BLL.Enums;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using BE.QuizByGPT.Interfaces;

namespace BE.QuizByGPT.Services
{
    public class QuizSessionService : IQuizSessionService
    {
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IQuizSessionRepository _quizSessionRepository;
        private readonly IQuizRepository _quizRepository;

        public QuizSessionService(IUserSessionRepository userSessionRepository, IQuizSessionRepository quizSessionRepository, IQuizRepository quizRepository)
        {
            _userSessionRepository = userSessionRepository;
            _quizSessionRepository = quizSessionRepository;
            _quizRepository = quizRepository;
        }

        public async Task<QuizSessionModel> Get(Guid id)
        {
            var quiz = await _quizSessionRepository.GetAsync(id);
            return quiz ?? throw new Exception("QuizSession not found.");
        }

        public async Task<QuizSessionModel> GetExtended(Guid id)
        {
            var quiz = await _quizSessionRepository.GetExtendedAsync(id);
            return quiz ?? throw new Exception("QuizSession not found.");
        }

        public async Task<QuizSessionModel> CreateByQuizId(Guid userSessionId, Guid quizId)
        {
            var userSession = await _userSessionRepository.GetAsync(userSessionId);
            if (userSession == null)
            {
                throw new Exception("UserSession not found.");
            }

            var quiz = await _quizRepository.GetAsync(quizId);
            if (quiz == null)
            {
                throw new Exception("Quiz not found.");
            }

            var quizSession = new QuizSessionModel
            {
                QuizId = quiz.Id,
                CreatedBy = userSession.Id,
                CreatedAt = DateTime.UtcNow,
                Status = QuizSessionStatusEnum.Awaiting
            };

            var createdQuizSession = await _quizSessionRepository.CreateAsync(quizSession);
            return createdQuizSession;
        }

        public async Task<QuizSessionModel> AttachUserToQuizSession(Guid userSessionId, Guid quizSessionId)
        {
            var userSession = await _userSessionRepository.GetAsync(userSessionId);
            if (userSession == null)
            {
                throw new Exception("UserSession not found.");
            }

            var quizSession = await _quizSessionRepository.GetExtendedAsync(quizSessionId);
            if (quizSession == null)
            {
                throw new Exception("QuizSession not found.");
            }

            quizSession.UserSessions ??= new();

            quizSession.UserSessions.Add(userSession);

            var updatedQuizSession = await _quizSessionRepository.UpdateAsync(quizSession);

            return updatedQuizSession;
        }

        public async Task UpdateUserReady(Guid userSessionId, Guid quizSessionId, bool isReady)
        {
            var userSession = await _userSessionRepository.GetAsync(userSessionId);
            if (userSession == null)
            {
                throw new Exception("UserSession not found.");
            }

            var userSessionQuizSession = userSession.UserSessionQuizSession?.FirstOrDefault(model => model.QuizSessionId == quizSessionId);
            if (userSessionQuizSession == null)
            {
                throw new Exception("The user does not exist in this quiz session.");
            }

            userSessionQuizSession.IsUserReady = isReady;

            await _userSessionRepository.UpdateAsync(userSession);
        }

        public async Task<QuizSessionModel> Start(Guid userSessionId, Guid quizSessionId)
        {
            var userSession = await _userSessionRepository.GetAsync(userSessionId);
            if (userSession == null)
            {
                throw new Exception("UserSessionId not found.");
            }

            var quizSession = await _quizSessionRepository.GetExtendedAsync(quizSessionId);
            if (quizSession == null)
            {
                throw new Exception("QuizSessionId not found.");
            }

            var userSessionQuizSession = quizSession.UserSessionQuizSession?.Where(model => model.QuizSessionId == quizSessionId).ToList();
            
            if (userSessionQuizSession == null || (userSessionQuizSession != null && userSessionQuizSession.Any(model => !model.IsUserReady)))
            {
                throw new Exception("Not all users are ready.");
            }

            quizSession.Status = QuizSessionStatusEnum.InProgress;

            await _quizSessionRepository.UpdateAsync(quizSession);

            return quizSession;
        }
    }
}
