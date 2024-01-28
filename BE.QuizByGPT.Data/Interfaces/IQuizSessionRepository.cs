using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.DAL.Interfaces;

public interface IQuizSessionRepository
{
    Task<QuizSessionModel> CreateAsync(QuizSessionModel userSession);
    Task<QuizSessionModel?> GetAsync(Guid id);
    Task<QuizSessionModel> UpdateAsync(QuizSessionModel userSession);
    Task DeleteAsync(Guid id);
}