using BE.QuizByGPT.BLL.Models;

namespace BE.QuizByGPT.DAL.Interfaces;

public interface IQuizRepository
{
    Task<QuizModel> CreateAsync(QuizModel userSession);
    Task<QuizModel?> GetAsync(Guid id);
    Task<QuizModel> UpdateAsync(QuizModel userSession);
    Task DeleteAsync(Guid id);
}