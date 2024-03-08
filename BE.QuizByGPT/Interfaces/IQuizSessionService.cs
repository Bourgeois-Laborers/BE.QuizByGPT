using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.Interfaces;

public interface IQuizSessionService
{
    Task<QuizSessionModel> Get(Guid id);
    Task<QuizSessionModel> CreateByQuizId(Guid userSessionId, Guid quizId);
    Task<QuizSessionModel> AttachUserToQuizSession(Guid userSessionId, Guid quizSessionId);
    Task UpdateUserReady(Guid userSessionId, Guid quizSessionId, bool isReady);
    Task<QuizSessionModel> Start(Guid userSessionId, Guid quizSessionId);
}
